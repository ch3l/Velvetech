﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidatableEntity<TKey, TValidator> : Entity<TKey>, IValidatableEntity
		where TValidator : Validator
	{
		private TValidator _validate;

		protected TValidator Validate
		{
			get => _validate ?? throw new Exception(
				$"Validator \"{typeof(TValidator).FullName}\" has not been selected " +
				$"in entity \"{this.GetType().FullName}\" " +
				$"using \"{nameof(SelectValidator)}\" method");
			private set => _validate = value;
		}

		public bool HasValidationErrors => _validate?.HasValidationErrors ?? false;
		public bool HasValidator => _validate != null;
		public IDictionary<string, string[]> Errors => _validate?.Errors ?? new Dictionary<string, string[]>();

		/// <summary>
		/// Only once sets validator instance 
		/// </summary>
		/// <param name="validator"></param>
		public void SelectValidator([NotNull] TValidator validator)
		{
			if (_validate != null)
				throw new Exception("Validator selected already");

			Validate = validator ?? throw new ArgumentNullException(nameof(validator));
		}
	}
}
