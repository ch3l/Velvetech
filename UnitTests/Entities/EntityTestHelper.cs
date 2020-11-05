
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.UnitTests.Entities
{
	public static class EntityTestHelper
	{
		public static void CheckErrorType<TTargetValidationError>(ValidationError error)
			where TTargetValidationError : ValidationError => 
			Assert.AreEqual(typeof(TTargetValidationError), error.GetType(),
				$"Current error type \"{error.GetType().Name}\" " +
				$"not equals to target error type \"{typeof(TTargetValidationError).Name}\"");

		public static void CheckErrorsCount(int targetErrorsCount, ValidationError[] errors, string className, string propertyName) => 
			Assert.AreEqual(targetErrorsCount, errors.Length,
				$"Errors count \"{errors.Length}\" after validation {className}'s property \"{propertyName}\" " +
				$"not equals to target errors count {targetErrorsCount}");
		 
		public static TTargetValidationError CheckForSingleError<TTargetValidationError>(string className, string propertyName, ValidationError[] errors)
			where TTargetValidationError : ValidationError
		{
			CheckErrorsCount(1, errors, className, propertyName);

			var error = errors[0];
			CheckErrorType<TTargetValidationError>(error);

			return (TTargetValidationError)error;
		}

		/// <summary>
		/// Not triggers if boundary crossed
		/// </summary>
		/// <param name="upperBoundary"></param>
		/// <param name="error"></param>
		public static void CheckUpperBoundaryCross(int upperBoundary, LengthComparisonValidationError error)
		{
			Assert.AreEqual(upperBoundary, error.ComparisonValue,
				$"Target upper boundary value \"{upperBoundary}\" differs from " +
				$"value \"{error.ComparisonValue}\" specified in {nameof(LengthComparisonValidationError)}");

			var targetComparisonResult = ComparisonResultType.More;

			Assert.AreEqual(targetComparisonResult, error.ComparisonResult,
				$"Expected comparison result \"{targetComparisonResult}\" differs from " +
				$"comparison result in {nameof(LengthComparisonValidationError)}");
		}

		/// <summary>
		/// Not triggers if boundary crossed
		/// </summary>
		/// <param name="lowerBoundary"></param>
		/// <param name="error"></param>
		public static void CheckLowerBoundaryCross(int lowerBoundary, LengthComparisonValidationError error)
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
