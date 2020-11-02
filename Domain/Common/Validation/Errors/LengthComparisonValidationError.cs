using System;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
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