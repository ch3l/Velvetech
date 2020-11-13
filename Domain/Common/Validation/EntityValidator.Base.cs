using System;
using System.Collections.Generic;
using System.Linq;
using Velvetech.Domain.Common.Validation.Errors;

namespace Velvetech.Domain.Common.Validation
{
	partial class EntityValidator
	{
		protected class BaseValidation
		{
			private readonly EntityValidator _validator;

			public BaseValidation(EntityValidator validator)
			{
				_validator = validator;
			}

			public bool IsNull<TValue>(TValue value, string propertyName)
			{
				if (value is null)
				{
					_validator.ValidationFail(new NullValidationError(propertyName));
					return true;
				}

				return false;
			}

			public bool IsEmpty<TValue>(IEnumerable<TValue> sequence, string propertyName)
			{
				if (!sequence.Any())
				{
					_validator.ValidationFail(new EmptyValidationError<IEnumerable<TValue>>(propertyName, sequence));
					return true;
				}

				return false;
			}

			public bool EmptyAsNull(ref string value)
			{
				if (value == string.Empty)
				{
					value = null;
					return true;
				}

				return false;
			}

			public bool IsWhitespaces(string value, string propertyName)
			{
				if (value.Trim() == string.Empty)
				{
					_validator.ValidationFail(new WhitespacesValidationError(propertyName, value));
					return true;
				}

				return false;
			}

			public void IsLessThan<TValue>(TValue value, TValue compareTo, string propertyName)
				where TValue : IComparable<TValue>
			{
				if (value.CompareTo(compareTo) < 0)
					_validator.ValidationFail(
						new ComparisonValidationError<TValue>(
							propertyName,
							value,
							compareTo,
							ComparisonResultType.Less));
			}

			public void IsMoreThan<TValue>(TValue value, TValue compareTo, string propertyName)
				where TValue : IComparable<TValue>
			{
				if (value.CompareTo(compareTo) > 0)
					_validator.ValidationFail(
						new ComparisonValidationError<TValue>(
							propertyName,
							value,
							compareTo,
							ComparisonResultType.More));
			}

			public void IsOutOfRange<TValue>(TValue value, TValue min, TValue max, string propertyName)
				where TValue : IComparable<TValue>
			{
				IsLessThan(value, min, propertyName);
				IsMoreThan(value, max, propertyName);
			}

			public void IsBiggerThan<T>(ICollection<T> value, int comparisonValue, string propertyName)
			{
				if (value.Count > comparisonValue)
					_validator.ValidationFail(
						new LengthComparisonValidationError(
							propertyName,
							value.Count,
							comparisonValue,
							ComparisonResultType.More));
			}

			public void IsLongerThan(string value, int comparisonValue, string propertyName)
			{
				if (value.Length > comparisonValue)
					_validator.ValidationFail(
						new LengthComparisonValidationError(
							propertyName,
							value.Length,
							comparisonValue,
							ComparisonResultType.More));
			}

			public void IsSmallerThan<T>(ICollection<T> value, int comparisonValue, string propertyName)
			{
				if (value.Count < comparisonValue)
					_validator.ValidationFail(
						new LengthComparisonValidationError(
							propertyName,
							value.Count,
							comparisonValue,
							ComparisonResultType.Less));
			}

			public void IsShorterThan(string value, int comparisonValue, string propertyName)
			{
				if (value.Length < comparisonValue)
					_validator.ValidationFail(
						new LengthComparisonValidationError(
							propertyName,
							value.Length,
							comparisonValue,
							ComparisonResultType.Less));
			}

			public void IsSizeOutOfRange<TValue>(ICollection<TValue> value, int minLength, int maxLength, string propertyName)
			{
				IsSmallerThan(value, minLength, propertyName);
				IsBiggerThan(value, maxLength, propertyName);
			}

			public void IsLengthOutOfRange(string value, int minLength, int maxLength, string propertyName)
			{
				IsShorterThan(value, minLength, propertyName);
				IsLongerThan(value, maxLength, propertyName);
			}
		}
	}
}
