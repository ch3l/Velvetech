using System.Collections.Generic;
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
		[TestMethod]
		public void SetNameTestIsNull()
		{
			SetNameTest<NullValidationError>(null, 1);
		}

		[TestMethod]
		public void SetNameTestIsEmpty()
		{
			SetNameTest<EmptyValidationError<IEnumerable<char>>>("", 1);
		}

		[TestMethod]
		public void SetNameTestIsWhitespaces()
		{
			SetNameTest<WhitespacesValidationError>("       ", 1);
		}

		[TestMethod]
		public void SetNameTestIsLongerThan25Chars()
		{
			//SetNameTest<LengthComparisonValidationError>("       ", 1);
		}

		[TestMethod]
		public void SetNameTest<TTargetValidationError>(string value, int targetErrorsCount)
			where TTargetValidationError : ValidationError
		{
			var className = nameof(Group);
			var propertyName = nameof(Group.Name);

			var group = new Group();
			var validator = new GroupValidator();
			group.SelectValidator(validator);
			
			Assert.AreEqual(true, group.HasValidator,
				$"{className}'s validator not initialized");

			group.SetName(value);

			Assert.AreEqual(true, group.Errors.ContainsKey(propertyName),
				$"{className}'s property \"{propertyName}\" not found in errors list");

			Assert.AreEqual(targetErrorsCount, group.Errors[propertyName].Length,
				$"Errors count after validation {className}'s property \"{propertyName}\" are not equal {targetErrorsCount}");

			if (targetErrorsCount != group.Errors[propertyName].Length) 
				return;
			
			var error = group.Errors[propertyName][0];

			if (error is TTargetValidationError currentError)
			{
				Assert.AreEqual(propertyName, currentError.PropertyName,
					$"Property {propertyName} with value \"{currentError.PropertyName}\" are not equals " +
					$"{propertyName}");
			}
			else
			{
				Assert.AreEqual(typeof(TTargetValidationError), error.GetType(),
					$"Current error type \"{error.GetType().Name}\" " +
					$"not equals to target error type \"{typeof(TTargetValidationError).Name}\"");
			}
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