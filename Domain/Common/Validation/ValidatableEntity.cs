using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidatableEntity<TKey, TValidator> : Entity<TKey>, IValidatableEntity
		where TValidator : Validator
	{ 
		protected TValidator Validate { get; private set; }

		public bool HasValidationErrors => Validate?.HasValidationErrors ?? false;
		public bool HasValidator => Validate != null;
		public IDictionary<string, string[]> Errors => Validate?.Errors ?? new Dictionary<string, string[]>();

		/// <summary>
		/// Only once sets validator instance 
		/// </summary>
		/// <param name="validator"></param>
		public void SelectValidator([NotNull] TValidator validator)
		{
			if (Validate != null)
				throw new Exception("Validator selected already");

			Validate = validator ?? throw new ArgumentNullException(nameof(validator));
		}
	}
}
