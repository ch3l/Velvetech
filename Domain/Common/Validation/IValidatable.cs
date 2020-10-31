using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace Velvetech.Domain.Common.Validation
{
	public interface IValidatableEntity
	{
		bool HasValidationErrors { get; }
		public IDictionary<string, string[]> Errors { get; }
	}

	public class ValidationEntryPoint<TEntity, TKey>
		where TEntity : Entity<TKey>
	{
		internal ValidatableEntity<TEntity, TKey> Entity { get; private set; }

		public ValidationEntryPoint([NotNull] ValidatableEntity<TEntity, TKey> entity)
		{
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
		}
	}

	public abstract class ValidatableEntity<TEntity, TKey> : Entity<TKey>, IValidatableEntity
		where TEntity : Entity<TKey>
	{
		private Dictionary<string, List<string>> _errors;
		private ValidationEntryPoint<TEntity, TKey> _validation;

		public bool HasValidationErrors => _errors != null && _errors.Count > 0;

		protected internal void ValidationFail(string propertyName, string error)
		{
			_errors ??= new Dictionary<string, List<string>>();

			if (!_errors.TryGetValue(propertyName, out var errorsList))
			{
				errorsList = new List<string>();
				_errors[propertyName] = errorsList;
			}

			errorsList.Add(error);
		}

		public IDictionary<string, string[]> Errors =>
			_errors is null
				? new Dictionary<string, string[]>()
				: _errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.ToArray());

		protected ValidationEntryPoint<TEntity, TKey> Validation
		{
			get => _validation ??= new ValidationEntryPoint<TEntity, TKey>(this);
			set => _validation = value;
		}
	}
}
