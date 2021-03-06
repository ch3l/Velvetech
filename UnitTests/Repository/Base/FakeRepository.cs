﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Ardalis.Specification;

using JetBrains.Annotations;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Exceptions;
using Velvetech.Domain.Common.Validation.Interfaces;
using BindingFlags = System.Reflection.BindingFlags;

namespace Velvetech.UnitTests.Repository.Base
{
	abstract class FakeRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		private readonly IDictionary<TKey, TEntity> _items = new Dictionary<TKey, TEntity>();

		protected abstract TKey NewKey();

		private void CheckSpecification<T>(ISpecification<T> specification)
		{
			if (specification != null)
				throw new NotSupportedException(specification.GetType().Name);
		}

		public async Task<TEntity> GetById(TKey id)
		{
			if (_items.TryGetValue(id, out var item))
				return await Task.FromResult(item);
			return await Task.FromResult((TEntity)null);
		}

		public async Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification)
		{
			CheckSpecification(specification);

			if (_items.TryGetValue(id, out var item))
				return await Task.FromResult(item);
			return await Task.FromResult((TEntity)null);
		}

		public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> condition, ISpecification<TEntity> specification)
		{
			CheckSpecification(specification);

			return await FirstOrDefault(condition);
		}

		public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> condition)
		{
			return await Task.FromResult(_items.Values.FirstOrDefault(value => condition.Compile().Invoke(value)));
		}

		public async IAsyncEnumerable<TEntity> ListAsync()
		{
			foreach (var item in _items.Values)
				yield return await Task.FromResult(item);
		}

		public IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification)
		{
			CheckSpecification(specification);

			return ListAsync();
		}

		public async Task<int> CountAsync()
		{
			return await Task.FromResult(_items.Count);
		}

		public Task<int> CountAsync(ISpecification<TEntity> specification)
		{
			CheckSpecification(specification);

			return CountAsync();
		}

		void CheckIfValidatable(TEntity entity)
		{
			if (entity is IValidatableEntity validatableEntity)
			{
				if (validatableEntity.HasErrors)
					throw new MissedErrorsValidationProcessingException(validatableEntity);
			}
		}

		public TEntity NewEntity()
		{
			return (TEntity) Activator.CreateInstance(typeof(TEntity), true);
		}

		public async Task<TEntity> AddAsync([NotNull] TEntity entity)
		{
			CheckIfValidatable(entity);

			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var properties = entity.GetType().GetProperties()
				.Where(property => 
					property.CanWrite && 
					property.CanRead &&
					property.Name != nameof(Entity<TKey>.Id));

			var newEntity = NewEntity();
			var newKey = NewKey();

			typeof(Entity<TKey>).GetProperty(nameof(Entity<TKey>.Id)).SetValue(newEntity, newKey);

			foreach (var property in properties)
				property.SetValue(newEntity, property.GetValue(entity));

			_items.Add(newKey, newEntity);

			return await Task.FromResult(newEntity);
		}

		public async Task UpdateAsync(TEntity entity)
		{
			CheckIfValidatable(entity);

			if (!_items.TryGetValue(entity.Id, out var savedEntity))
				throw new KeyNotFoundException(nameof(Entity<TKey>.Id));

			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var properties = entity.GetType().GetProperties()
				.Where(property => property.CanWrite && property.CanRead);

			foreach (var property in properties)
				property.SetValue(savedEntity, property.GetValue(entity));

			_items[savedEntity.Id] = savedEntity;

			await Task.CompletedTask;
		}

		public async Task RemoveAsync(TEntity entity)
		{
			await Task.FromResult(_items.Remove(entity.Id));
		}

		public async Task RemoveRangeAsync(TEntity[] entities)
		{
			foreach (var entity in entities)
				await RemoveAsync(entity);
		}
	}
}
