using System;
using System.Collections.Generic;
using System.Linq;

namespace Velvetech.Domain.Common.Validation
{
	public class Validator
	{
		private Dictionary<string, List<string>> _textErrors;
		private Dictionary<string, List<ValidationError>> _errors;

		public bool HasValidationErrors => _textErrors != null && _textErrors.Count > 0;

		public IDictionary<string, string[]> Errors =>
			_textErrors is null
				? new Dictionary<string, string[]>()
				: _textErrors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.ToArray());

		protected void ValidationFail(string propertyName, string error)
		{
			_textErrors ??= new Dictionary<string, List<string>>();

			if (!_textErrors.TryGetValue(propertyName, out var errorsList))
			{
				errorsList = new List<string>();
				_textErrors[propertyName] = errorsList;
			}

			errorsList.Add(error);
		}

		protected void ValidationFail<TValue>(ValidationError validationError)
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
				ValidationFail(propertyName, $"Is null");
				return true;
			}

			return false;
		}

		public bool IsEmpty<TValue>(IEnumerable<TValue> sequence, string propertyName)
		{
			if (!sequence.Any())
			{
				ValidationFail(propertyName, $"Is empty");
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
				ValidationFail(propertyName, $"Consists of whitespaces only");
		}

		public void IsLess<TValue>(TValue value, TValue compareTo, string propertyName)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(compareTo) < 0)
				ValidationFail(propertyName, $"Is less than {compareTo}");
		}

		public void IsExceed<TValue>(TValue value, TValue compareTo, string propertyName)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(compareTo) > 0)
				ValidationFail(propertyName, $"Exceeds {compareTo}");
		}

		public void IsOutOfRange<TValue>(TValue value, TValue min, TValue max, string propertyName)
			where TValue : IComparable<TValue>
		{
			IsLess(value, min, propertyName);
			IsExceed(value, max, propertyName);
		}

		public void IsExceedSize<T>(ICollection<T> value, int compareTo, string propertyName)
		{
			if (value.Count > compareTo)
				ValidationFail(propertyName, $"Length exceeds {compareTo}");
		}

		public void IsExceedSize(string value, int compareTo, string propertyName)
		{
			if (value.Length> compareTo)
				ValidationFail(propertyName, $"Length exceeds {compareTo}");
		}

		public void IsLessThanSize<T>(ICollection<T> value, int compareTo, string propertyName)
		{
			if (value.Count < compareTo)
				ValidationFail(propertyName, $"Length is less than {compareTo}");
		}

		public void IsLessThanSize(string value, int compareTo, string propertyName)
		{
			if (value.Length < compareTo)
				ValidationFail(propertyName, $"Length is less than {compareTo}");
		}

		public void IsCountOutOfRange<TValue> (ICollection<TValue> value, int minLength, int maxLength, string propertyName)
		{
			IsLessThanSize(value, minLength, propertyName);
			IsExceedSize(value, maxLength, propertyName);
		}

		public void IsCountOutOfRange(string value, int minLength, int maxLength, string propertyName)
		{
			IsLessThanSize(value, minLength, propertyName);
			IsExceedSize(value, maxLength, propertyName);
		}
	}
}