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
		public TValue ComparisonValue { get; }

		public ComparisonValidationError(string propertyName, TValue value, TValue comparionValue, ComparisonResultType comparisonResult)
			: base(propertyName, value)
		{
			ComparisonResult = comparisonResult;
			ComparisonValue = comparionValue;
		}

		public override string ToString()
		{
			return ComparisonResult switch
			{
				ComparisonResultType.Less => $"{Value} is {ComparisonResult} than {ComparisonValue}",
				ComparisonResultType.More => $"{Value} is {ComparisonResult} than {ComparisonValue}",
				_ => throw new ArgumentOutOfRangeException(nameof(ComparisonResult))
			};
		}
	}

	public class LengthComparisonValidationError<TValue> : DetailedValidationError<TValue>
	{
		public ComparisonResultType ComparisonResult { get; }
		public TValue ComparisonValue { get; }

		public LengthComparisonValidationError(string propertyName, TValue value, TValue comparisonValue, ComparisonResultType comparisonResult)
			: base(propertyName, value)
		{
			ComparisonResult = comparisonResult;
			ComparisonValue = comparisonValue;
		}

		public override string ToString()
		{
			return ComparisonResult switch
			{
				ComparisonResultType.Less => $"Length is {ComparisonResult} than {ComparisonValue}",
				ComparisonResultType.More => $"Length is {ComparisonResult} than {ComparisonValue}",
				_ => throw new ArgumentOutOfRangeException(nameof(ComparisonResult))
			};
		}
	}
}
