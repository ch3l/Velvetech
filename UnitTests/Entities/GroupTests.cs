using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class GroupTests
	{
		private const string ClassName = nameof(Group);
		private const string PropertyName = nameof(Group.Name);
		private const int UpperLengthBoundary = 25;

		[TestMethod]
		public void SetNameTestIsNull()
		{
			string value = null;

			var errors = GetErrorsOfSetName(value);
			CheckErrorsCount(1, errors, ClassName, PropertyName);
			
			var error = errors[0];
			CheckErrorType<NullValidationError>(error);
		}

		[TestMethod]
		public void SetNameTestIsEmpty()
		{
			string value = "";

			var errors = GetErrorsOfSetName(value);
			CheckErrorsCount(1, errors, ClassName, PropertyName);

			var error = errors[0];
			CheckErrorType<EmptyValidationError<IEnumerable<char>>>(error);
		}

		[TestMethod]
		public void SetNameTestForWhitespacesAndBoundary()
		{
			string upperBoundaryValue = new string(Enumerable.Range(1,UpperLengthBoundary).Select(x => ' ').ToArray());
			string longerValue = new string(Enumerable.Range(1, UpperLengthBoundary+1).Select(x => ' ').ToArray());

			// Checking upper boundary without crossing
			{
				var errors = GetErrorsOfSetName(upperBoundaryValue);
				CheckErrorsCount(1, errors, ClassName, PropertyName);

				var error = errors[0];
				CheckErrorType<WhitespacesValidationError>(error);
			}

			// Checking upper boundary cross
			{
				var errors = GetErrorsOfSetName(longerValue);
				CheckErrorsCount(1, errors, ClassName, PropertyName);

				var error = errors[0];
				CheckErrorType<WhitespacesValidationError>(error);
			}
		}

		[TestMethod]
		public void SetNameTestLengthUpperBoundary()
		{
			string boundaryValue = new string(Enumerable.Range(1, UpperLengthBoundary).Select(x => 'X').ToArray());
			string longerValue = new string(Enumerable.Range(1, UpperLengthBoundary+1).Select(x => 'X').ToArray());

			// Checking upper boundary without crossing
			{
				var errors = GetErrorsOfSetName(boundaryValue);
				CheckErrorsCount(0, errors, ClassName, PropertyName);
			}

			// Checking upper boundary cross
			{
				var errors = GetErrorsOfSetName(longerValue);
				CheckErrorsCount(1, errors, ClassName, PropertyName);
				
				var error = errors[0];
				CheckErrorType<LengthComparisonValidationError>(error);

				var lengthComparisonError = (LengthComparisonValidationError)error;
				Assert.AreEqual(25, lengthComparisonError.ComparisonValue);
				Assert.AreEqual(ComparisonResultType.More, lengthComparisonError.ComparisonResult);
			}
		}

		[TestMethod]
		ValidationError[] GetErrorsOfSetName(string value)
		{
			var group = new Group();
			var validator = new GroupValidator();
			group.SelectValidator(validator);

			Assert.AreEqual(true, group.HasValidator,
				$"{ClassName}'s validator not initialized");

			group.SetName(value);

			if (group.Errors.TryGetValue(PropertyName, out var errors))
				return errors;
			return new ValidationError[0];
		}

		void CheckErrorType<TTargetValidationError>(ValidationError error)
			where TTargetValidationError : ValidationError
		{
			Assert.AreEqual(typeof(TTargetValidationError), error.GetType(),
				$"Current error type \"{error.GetType().Name}\" " +
				$"not equals to target error type \"{typeof(TTargetValidationError).Name}\"");
		}

		void CheckErrorsCount(int targetErrorsCount, ValidationError[] errors, string className, string propertyName)
		{
			Assert.AreEqual(targetErrorsCount, errors.Length,
				$"Errors count \"{errors.Length}\" after validation {className}'s property \"{propertyName}\" " +
				$"not equals to target errors count {targetErrorsCount}");
		}

		[TestMethod()]
		public void IncludeStudentTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ExcludeStudentTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ExcludeAllStudentsTest()
		{
			Assert.Fail();
		}
	}
}