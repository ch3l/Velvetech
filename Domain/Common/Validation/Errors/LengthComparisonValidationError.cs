using System;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	public class LengthComparisonValidationError : DetailedValidationError<int>
	{
		public ComparisonResultType ComparisonResult { get; }
		public int ComparisonValue { get; }

		public LengthComparisonValidationError(string propertyName, int value, int comparisonValue, ComparisonResultType comparisonResult)
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