using System.Collections.Generic;

using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidatableEntity<TKey, TValidator> : Entity<TKey>, IValidatableEntity
		where TValidator : Validator, new()
	{
		private TValidator _validation;

		protected TValidator Validate
		{
			get => _validation ??= new TValidator();
			set => _validation = value;
		}

		public bool HasValidationErrors => Validate?.HasValidationErrors ?? false;
		public IDictionary<string, string[]> Errors => Validate?.Errors ?? new Dictionary<string, string[]>();
	}
}
