using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Common.Validation
{
	public abstract class ValidationError
	{
		protected ValidationError(string propertyName)
		{
			PropertyName = propertyName;
		}

		/// <summary>
		/// Target property propertyName
		/// </summary>
		public string PropertyName { get; }

		public abstract override string ToString();
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

		public abstract string ToDetailedString();
	}

	public class NullValidationError: ValidationError
	{
		public NullValidationError(string propertyName) 
			: base(propertyName)
		{
		}

		public override string ToString()
		{
			return "Is null";
		}
	}

	public class EmptyValidationError<TValue> : DetailedValidationError<TValue>
	{
		public EmptyValidationError(string propertyName, TValue value)
			: base(propertyName, value)
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

	public class WhitespacesValidationError : DetailedValidationError<string>
	{
		public WhitespacesValidationError(string propertyName, string value)
			: base(propertyName, value)
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

	public enum ComparisonResultType
	{
		Equals,
		Less,
		More
	}

	public class ComparisonValidationError<TValue> : DetailedValidationError<TValue>
	{
		public ComparisonResultType ComparisonResult { get; }
		public TValue ComparedWithValue { get; }

		public ComparisonValidationError(string propertyName, TValue value, TValue comparedWithValue, ComparisonResultType comparisonResult)
			: base(propertyName, value)
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
				ComparisonResultType.Equals => $"{PropertyName} value \"{Value}\" {ComparisonResult} border value {Value}",
				ComparisonResultType.Less => $"{PropertyName} value \"{Value}\" is {ComparisonResult} than {Value}",
				ComparisonResultType.More => $"{PropertyName} value \"{Value}\" is {ComparisonResult} than {Value}",
				_ => throw new ArgumentOutOfRangeException(nameof(ComparisonResult))
			};
		}
	}

	public class LengthComparisonValidationError<TValue> : DetailedValidationError<TValue>
	{
		public ComparisonResultType ComparisonResult { get; }
		public TValue ComparedWithValue { get; }

		public LengthComparisonValidationError(string propertyName, TValue value, TValue comparedWithValue, ComparisonResultType comparisonResult)
			: base(propertyName, value)
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
				ComparisonResultType.Equals => $"{PropertyName} length \"{Value}\" {ComparisonResult} border value {ComparedWithValue}",
				ComparisonResultType.Less => $"{PropertyName} value \"{Value}\" is {ComparisonResult} than {Value}",
				ComparisonResultType.More => $"{PropertyName} value \"{Value}\" is {ComparisonResult} than {Value}",
				_ => throw new ArgumentOutOfRangeException(nameof(ComparisonResult))
			};
		}
	}
}
