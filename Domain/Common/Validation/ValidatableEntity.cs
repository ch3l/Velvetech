using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidatableEntity<TKey, TValidator> : Entity<TKey>, IValidatableEntity
		where TValidator : Validator, new()
	{
		private TValidator _validation;

		protected TValidator Validator
		{
			get => _validation ??= new TValidator();
			set => _validation = value;
		}

		public bool HasValidationErrors => Validator?.HasValidationErrors ?? false;
		public IDictionary<string, string[]> Errors => Validator?.Errors ?? new Dictionary<string, string[]>();
	}
}
