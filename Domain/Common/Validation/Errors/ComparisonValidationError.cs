using System;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
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
}