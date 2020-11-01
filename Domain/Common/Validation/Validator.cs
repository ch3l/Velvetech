using System;
using System.Collections.Generic;
using System.Linq;

namespace Velvetech.Domain.Common.Validation
{
	public class Validator
	{
		private Dictionary<string, List<string>> _errors;

		public bool HasValidationErrors => _errors != null && _errors.Count > 0;

		public IDictionary<string, string[]> Errors =>
			_errors is null
				? new Dictionary<string, string[]>()
				: _errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.ToArray());

		protected void ValidationFail(string propertyName, string error)
		{
			_errors ??= new Dictionary<string, List<string>>();

			if (!_errors.TryGetValue(propertyName, out var errorsList))
			{
				errorsList = new List<string>();
				_errors[propertyName] = errorsList;
			}

			errorsList.Add(error);
		}

		public bool IsNull<TValue>(TValue value, string propertyName)
		{
			if (value is null)
			{
				ValidationFail(propertyName, $"{propertyName} is null");
				return true;
			}

			return false;
		}

		public void IsEmpty<TValue>(IEnumerable<TValue> sequence, string propertyName)
		{
			if (!sequence.Any())
				ValidationFail(propertyName, $"{propertyName} is empty");
		}

		public void IsWhitespaces(string value, string propertyName)
		{
			if (value.Trim() == string.Empty)
				ValidationFail(propertyName, $"{propertyName}'s value \"{value}\" consists of whitespaces only");
		}

		public void IsLess<TValue>(TValue value, TValue compareTo, string propertyName)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(compareTo) < 0)
				ValidationFail(propertyName, $"{propertyName}'s value\"{value}\" is less than {compareTo}");
		}

		public void IsExceed<TValue>(TValue value, TValue compareTo, string propertyName)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(compareTo) > 0)
				ValidationFail(propertyName, $"{propertyName}'s value\"{value}\" exceeds {compareTo}");
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
				ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" exceeds {compareTo}");
		}

		public void IsLessThanSize<T>(ICollection<T> value, int compareTo, string propertyName)
		{
			if (value.Count < compareTo)
				ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is less than {compareTo}");
		}

		public void IsCountOutOfRange<TValue> (ICollection<TValue> value, int minLength, int maxLength, string propertyName)
		{
			IsLessThanSize(value, minLength, propertyName);
			IsExceedSize(value, maxLength, propertyName);
		}
	}
}