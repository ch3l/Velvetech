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

		public void IsInRange<TValue>(TValue value, TValue min, TValue max, string propertyName)
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(min) < 0)
				ValidationFail(propertyName, $"{propertyName}'s value\"{value}\" is less than {min}");

			if (value.CompareTo(max) > 0)
				ValidationFail(propertyName, $"{propertyName}'s value\"{value}\" is over {max}");
		}

		public void IsWhitespaces(string value, string propertyName)
		{
			if (value.Trim() == string.Empty)
				ValidationFail(propertyName, $"{propertyName}'s value \"{value}\" consists of whitespaces only");
		}

		public void IsLengthOver(string value, int maxLength, string propertyName)
		{
			if (value.Length > maxLength)
				ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is over {maxLength}");
		}

		public void IsLengthLess(string value, int minLength, string propertyName)
		{
			if (value.Length < minLength)
				ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is less than {minLength}");
		}

		public void IsLengthInRage(string value, int minLength, int maxLength, string propertyName)
		{
			IsLengthLess(value, minLength, propertyName);
			IsLengthOver(value, maxLength, propertyName);
		}
	}
}