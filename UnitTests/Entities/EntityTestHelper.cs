using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	}
}
