using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation
{
	public class EntityValidator
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

		public bool HasErrorsInProperty(string key)
		{
			return _errors != null && _errors.ContainsKey(key);
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

		public bool IsWhitespaces(string value, string propertyName)
		{
			if (value.Trim() == string.Empty)
			{
				ValidationFail(new WhitespacesValidationError(propertyName, value));
				return true;
			}

			return false;
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

		public void IsBiggerThan<T>(ICollection<T> value, int comparisonValue, string propertyName)
		{
			if (value.Count > comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError(
						propertyName,
						value.Count,
						comparisonValue,
						ComparisonResultType.More));
		}

		public void IsLongerThan(string value, int comparisonValue, string propertyName)
		{
			if (value.Length > comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError(
						propertyName,
						value.Length,
						comparisonValue,
						ComparisonResultType.More));
		}

		public void IsSmallerThan<T>(ICollection<T> value, int comparisonValue, string propertyName)
		{
			if (value.Count < comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError(
						propertyName,
						value.Count,
						comparisonValue,
						ComparisonResultType.Less));
		}

		public void IsShorterThan(string value, int comparisonValue, string propertyName)
		{
			if (value.Length < comparisonValue)
				ValidationFail(
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