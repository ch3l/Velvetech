using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidationError
	{
		protected ValidationError(string name)
		{
			PropertyName = name;
		}

		/// <summary>
		/// Target property name
		/// </summary>
		public string PropertyName { get; }
	}

	public abstract class DetailedValidationError<TValue> : ValidationError
	{
		/// <summary>
		/// Not valid value
		/// </summary>
		public TValue Value { get; }

		protected DetailedValidationError(string propertyName, TValue value)
			: base(propertyName)
		{
			Value = value;
		}

		public abstract override string ToString();
		public abstract string ToDetailedString();
	}

	public class NullValidationError<TValue> : DetailedValidationError<TValue>
	{
		public NullValidationError(string name, TValue value) 
			: base(name, value)
		{
		}

		public override string ToString()
		{
			return "Is null";
		}

		public override string ToDetailedString()
		{
			return $"{PropertyName} is null";
		}
	}

	public class EmptyValidationError<TValue> : DetailedValidationError<TValue>
	{
		public EmptyValidationError(string name, TValue value)
			: base(name, value)
		{
		}

		public override string ToString()
		{
			return "Is empty";
		}

		public override string ToDetailedString()
		{
			return $"{PropertyName} value is empty";
		}
	}

	public class WhitespacesValidationError<TValue> : DetailedValidationError<TValue>
	{
		public WhitespacesValidationError(string name, TValue value)
			: base(name, value)
		{
		}

		public override string ToString()
		{
			return "Consists of whitespaces only";
		}

		public override string ToDetailedString()
		{
			return $"{PropertyName} value \"{Value}\" consists of whitespaces only";
		}
	}

	public class ComparisonValidationError<TValue> : DetailedValidationError<TValue>
	{
		public ComparisonResultType ComparisonResult { get; }
		public TValue ComparedWithValue { get; }

		public ComparisonValidationError(string name, TValue value, TValue comparedWithValue, ComparisonResultType comparisonResult)
			: base(name, value)
		{
			ComparisonResult = comparisonResult;
			ComparedWithValue = comparedWithValue;
		}

		public override string ToString()
		{
			return $"Is {ComparisonResult}";
		}

		public override string ToDetailedString()
		{
			return ComparisonResult switch
			{
				ComparisonResultType.Equals => $"{PropertyName} value \"{Value}\" {ComparisonResult}",
				ComparisonResultType.Less => $"{PropertyName} value \"{Value}\" is {ComparisonResult} than {Value}",
				ComparisonResultType.More => $"{PropertyName} value \"{Value}\" is {ComparisonResult} than {Value}",
				_ => throw new ArgumentOutOfRangeException(nameof(ComparisonResult))
			};
		}

		public enum ComparisonResultType
		{
			Equals,
			Less,
			More
		}
	}
}
