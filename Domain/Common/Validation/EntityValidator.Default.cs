using System;

namespace Velvetech.Domain.Common.Validation
{
	public partial class EntityValidator
	{
		protected class DefaultValidation
		{
			private readonly EntityValidator _validator;

			public DefaultValidation(EntityValidator validator)
			{
				_validator = validator;
			}

			public void DenyNullOrEmptyString(string propertyName, ref string value, int minLength, int maxLength)
			{
				if (minLength < 0)
					throw new ArgumentOutOfRangeException($"{nameof(minLength)} is less than 0");

				_validator.ClearErrors(propertyName);

				if (_validator.BaseValidations.IsNull(value, propertyName))
					return;

				if (_validator.BaseValidations.IsEmpty(value, propertyName))
					return;

				if (_validator.BaseValidations.IsWhitespaces(value, propertyName))
					return;

				value = value.Trim();

				if (minLength > 0) // 0 if previous conditions might be changed
					_validator.BaseValidations.IsShorterThan(value, minLength, propertyName);

				if (maxLength < int.MaxValue)
					_validator.BaseValidations.IsLongerThan(value, maxLength, propertyName);
			}

			public void DenyNullOrEmptyString(string propertyName, ref string value, int maxLength = int.MaxValue) =>
				DenyNullOrEmptyString(propertyName, ref value, 1, maxLength);

			public void AllowNullOrEmptyString(string propertyName, ref string value, int minLength, int maxLength)
			{
				if (minLength < 0)
					throw new ArgumentOutOfRangeException($"{nameof(minLength)} is less than 0");

				_validator.ClearErrors(propertyName);

				if (value is null)
					return;

				if (_validator.BaseValidations.EmptyAsNull(ref value))
					return;

				if (_validator.BaseValidations.IsWhitespaces(value, propertyName))
					return;

				value = value.Trim();

				if (minLength > 0) // 0 if previous conditions might be changed
					_validator.BaseValidations.IsShorterThan(value, minLength, propertyName);

				if (maxLength < int.MaxValue)
					_validator.BaseValidations.IsLongerThan(value, maxLength, propertyName);
			}

			public void AllowNullOrEmptyString(string propertyName, ref string value, int maxLength) =>
				AllowNullOrEmptyString(propertyName, ref value, 0, maxLength);
		}
	}
}
