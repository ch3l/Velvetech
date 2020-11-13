using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation
{
	public partial class EntityValidator
	{
		private Dictionary<string, HashSet<ValidationError>> _errors;

		public bool HasErrors => _errors != null && _errors.Count > 0;

		public IReadOnlyDictionary<string, string[]> ErrorsStrings =>
			new ReadOnlyDictionary<string, string[]>(
				_errors is null
					? new Dictionary<string, string[]>()
					: _errors.ToDictionary(
						pair => pair.Key,
						pair => pair.Value
							.Select(error => error.ToString())
							.ToArray()));

		public IReadOnlyDictionary<string, ValidationError[]> Errors =>
			new ReadOnlyDictionary<string, ValidationError[]>(
				_errors is null
					? new Dictionary<string, ValidationError[]>()
					: _errors.ToDictionary(
						pair => pair.Key,
						pair => pair.Value.ToArray()));

		protected DefaultValidation DefaultValidations { get; }
		protected BaseValidation BaseValidations { get; }

		public EntityValidator()
		{
			DefaultValidations = new DefaultValidation(this);
			BaseValidations = new BaseValidation(this);
		}

		public bool HasErrorsInProperty(string key)
		{
			return _errors != null && _errors.ContainsKey(key);
		}

		protected void ValidationFail(ValidationError validationError)
		{
			if (validationError == null) throw new ArgumentNullException(nameof(validationError));
			_errors ??= new Dictionary<string, HashSet<ValidationError>>();

			if (!_errors.TryGetValue(validationError.PropertyName, out var errorsSet))
			{
				errorsSet = new HashSet<ValidationError>();
				_errors[validationError.PropertyName] = errorsSet;
			}

			errorsSet.Add(validationError);
		}

		protected void ClearErrors(string key)
		{
			if (_errors != null &&
			    _errors.TryGetValue(key, out var errors))
			{
				errors?.Clear();
				_errors.Remove(key);
			}
		}

		
	}
}