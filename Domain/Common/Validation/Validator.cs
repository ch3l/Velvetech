using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation
{
	public class Validator
	{
		private Dictionary<string, List<ValidationError>> _errors;

		public bool HasValidationErrors => _errors != null && _errors.Count > 0;

		public IReadOnlyDictionary<string, string[]> ErrorsStrings => 
			new ReadOnlyDictionary<string, string[]>(
				_errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value
						.Select(error => error.ToString())
						.ToArray()));

		public IReadOnlyDictionary<string, ValidationError[]> Errors =>
			new ReadOnlyDictionary<string, ValidationError[]>(
				_errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.ToArray()));

		/*
		protected void ClearErrors(string propertyName)
		{
			if (_errors is null)
				return;
			
			if (_errors.TryGetValue(propertyName, out var errorsList))
				errorsList?.Clear();
				
		}
		*/

		protected void ValidationFail(ValidationError validationError)
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

		public void IsBiggerThan<T>(ICollection<T> value, int comparisonValue, string propertyName)
		{
			if (value.Count > comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName,
						value.Count,
						comparisonValue,
						ComparisonResultType.More));
		}

		public void IsLongerThan(string value, int comparisonValue, string propertyName)
		{
			if (value.Length > comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName,
						value.Length,
						comparisonValue,
						ComparisonResultType.More));
		}

		public void IsSmallerThan<T>(ICollection<T> value, int comparisonValue, string propertyName)
		{
			if (value.Count < comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError<int>(
						propertyName,
						value.Count,
						comparisonValue,
						ComparisonResultType.Less));
		}

		public void IsShorterThan(string value, int comparisonValue, string propertyName)
		{
			if (value.Length < comparisonValue)
				ValidationFail(
					new LengthComparisonValidationError<int>(
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