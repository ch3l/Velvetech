using System;
using System.Collections.Generic;
using System.Linq;

namespace Velvetech.Domain.Common.Validation
{
	public class Validator
	{
		private Dictionary<string, List<ValidationError>> _errors;

		public bool HasValidationErrors => _errors != null && _errors.Count > 0;

		public IDictionary<string, string[]> Errors =>
			_errors is null
				? new Dictionary<string, string[]>()
				: _errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.Select(error => error.ToString()).ToArray());

		private void ValidationFail(ValidationError validationError)
		{
			_errors ??= new Dictionary<string, List<ValidationError>>();

			if (!_errors.TryGetValue(validationError.PropertyName, out var errorsList))
			{
				errorsList = new List<ValidationError>();
				_errors[validationError.PropertyName] = errorsList;
			}

			errorsList.Add(validationError);
		}

		public bool IsNull<TValue>(TValue value, string propertyName)
		{
			if (value is null)
			{
				ValidationFail(new NullValidationError(propertyName));
				return true;
			}

			return false;
		}

		public bool IsEmpty<TValue>(IEnumerable<TValue> sequence, string propertyName)
		{
			if (!sequence.Any())
			{
				ValidationFail(new EmptyValidationError<IEnumerable<TValue>>(propertyName, sequence));
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

		public void IsWhitespaces(string value, string propertyName)
		{
			if (value.Trim() == string.Empty)
				ValidationFail(new WhitespacesValidationError(propertyName, value));
		}

		public void IsLessThan<TValue>(TValue value, TValue compareTo, string propertyName)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(compareTo) < 0)
				ValidationFail(
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
				ValidationFail(
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

		public void IsMoreThanSize<T>(ICollection<T> value, int compareTo, string propertyName)
		{
			if (value.Count > compareTo)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName, 
						value.Count,
						compareTo,
						ComparisonResultType.More));
		}

		public void IsMoreThanLength(string value, int compareTo, string propertyName)
		{
			if (value.Length > compareTo)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName,
						value.Length,
						compareTo,
						ComparisonResultType.More));
		}

		public void IsLessThanSize<T>(ICollection<T> value, int compareTo, string propertyName)
		{
			if (value.Count < compareTo)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName,
						value.Count,
						compareTo,
						ComparisonResultType.Less));
		}

		public void IsLessThanLength(string value, int compareTo, string propertyName)
		{
			if (value.Length < compareTo)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName,
						value.Length,
						compareTo,
						ComparisonResultType.Less));
		}

		public void IsSizeOutOfRange<TValue> (ICollection<TValue> value, int minLength, int maxLength, string propertyName)
		{
			IsLessThanSize(value, minLength, propertyName);
			IsMoreThanSize(value, maxLength, propertyName);
		}

		public void IsLengthOutOfRange(string value, int minLength, int maxLength, string propertyName)
		{
			IsLessThanLength(value, minLength, propertyName);
			IsMoreThanLength(value, maxLength, propertyName);
		}
	}
}