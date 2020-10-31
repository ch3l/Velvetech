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

	public class Validator<TEntity, TKey>
		where TEntity : Entity<TKey>
	{
		internal ValidatableEntity<TEntity, TKey> Entity { get; private set; }

		private Dictionary<string, List<string>> _errors;

		public bool HasValidationErrors => _errors != null && _errors.Count > 0;

		public IDictionary<string, string[]> Errors =>
			_errors is null
				? new Dictionary<string, string[]>()
				: _errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.ToArray());

		public Validator([NotNull] ValidatableEntity<TEntity, TKey> entity)
		{
			Entity = entity;
		}

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
	}

	public abstract class ValidatableEntity<TEntity, TKey> : Entity<TKey>, IValidatableEntity
		where TEntity : Entity<TKey>
	{
		private Validator<TEntity, TKey> _validation;

		protected Validator<TEntity, TKey> Validation
		{
			get => _validation ??= new Validator<TEntity, TKey>(this);
			set => _validation = value;
		}

		public bool HasValidationErrors => Validation.HasValidationErrors;
		public IDictionary<string, string[]> Errors => Validation.Errors;
	}
}
