﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Common.Validation.Exceptions;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidatableEntity<TKey, TValidator> : Entity<TKey>, IValidatableEntity
		where TValidator : EntityValidator
	{
		private TValidator _validate;

		protected TValidator Validate
		{
			get => _validate ?? throw new NotSelectedValidatorException(this);
			private set => _validate = value;
		}

		public bool HasErrors => _validate?.HasErrors ?? false;
		public bool HasValidator => _validate != null;

		public IReadOnlyDictionary<string, string[]> ErrorsStrings => _validate?.ErrorsStrings ?? new ReadOnlyDictionary<string, string[]>(new Dictionary<string, string[]>());
		public IReadOnlyDictionary<string, ValidationError[]> Errors => _validate?.Errors ?? new ReadOnlyDictionary<string, ValidationError[]>(new Dictionary<string, ValidationError[]>());

		/// <summary>
		/// Applies validator instance once
		/// </summary>
		/// <param name="validator"></param>
		public void SelectValidator([NotNull] TValidator validator)
		{
			if (_validate != null)
				throw new Exception("Validator selected already");

			Validate = validator ?? throw new ArgumentNullException(nameof(validator));
		}

		public bool HasErrorsInProperty(string key) => _validate?.HasErrorsInProperty(key) ?? false;

	}
}
