using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.UnitTests.Helpers
{
	public static class ValidationsTestHelper
	{
		public static ValidationError[] CheckErrorsCount(EntityValidator validator, string propertyName, int expectedErrorsCount)
		{
			var errorsDictionary = validator.Errors;
			
			if (errorsDictionary.TryGetValue(propertyName, out var errors))
			{
				Assert.AreEqual(expectedErrorsCount, errors.Length);
				return errors;
			}
			
			return new ValidationError[0];
		}

		public static void CheckErrorType<TTargetValidationError>(ValidationError[] errors, int errorIndex = 0)
			where TTargetValidationError : ValidationError
		{
			Assert.AreEqual(true, errors[errorIndex] is TTargetValidationError,
				$"{errors[errorIndex].GetType().Name} not equals " +
				$"{typeof(TTargetValidationError).Name}");
		}

		public static void CheckUpperBoundaryCross(LengthComparisonValidationError error, int upperBoundary)
		{
			Assert.AreEqual(upperBoundary, error.ComparisonValue,
				$"Target upper boundary value \"{upperBoundary}\" differs from " +
				$"value \"{error.ComparisonValue}\" specified in {nameof(LengthComparisonValidationError)}");

			var targetComparisonResult = ComparisonResultType.More;

			Assert.AreEqual(targetComparisonResult, error.ComparisonResult,
				$"Expected comparison result \"{targetComparisonResult}\" differs from " +
				$"comparison result in {nameof(LengthComparisonValidationError)}");
		}

		public static void CheckLowerBoundaryCross(LengthComparisonValidationError error, int lowerBoundary)
		{
			Assert.AreEqual(lowerBoundary, error.ComparisonValue,
				$"Target lower boundary value \"{lowerBoundary}\" differs from " +
				$"value \"{error.ComparisonValue}\" specified in {nameof(LengthComparisonValidationError)}");

			var targetComparisonResult = ComparisonResultType.Less;

			Assert.AreEqual(targetComparisonResult, error.ComparisonResult,
				$"Expected comparison result \"{targetComparisonResult}\" differs from " +
				$"comparison result in {nameof(LengthComparisonValidationError)}");
		}
	}
}
