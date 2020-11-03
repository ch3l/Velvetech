using Microsoft.VisualStudio.TestTools.UnitTesting;
using Velvetech.Domain.Common.Validation.Errors;
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
			const string className = nameof(Group);
			const int targetErrorsCount = 1;
			const string propertyName = nameof(Group.Name);

			var group = new Group();

			var validator = new GroupValidator();
			group.SelectValidator(validator);

			string newName = null;
			group.SetName(newName);

			Assert.AreEqual(true, group.Errors.ContainsKey(propertyName),
				$"{className}'s property \"{propertyName}\" not found in errors list");

			Assert.AreEqual(targetErrorsCount, group.Errors[propertyName].Length,
				$"Errors count after validation {className}'s property \"{propertyName}\" are not equal {targetErrorsCount}");

			var error = group.Errors[propertyName][0];

			if (error is NullValidationError nullValidationError)
			{
				Assert.AreEqual(propertyName, nullValidationError.PropertyName,
					$"Property {nameof(NullValidationError.PropertyName)} with value \"{nullValidationError.PropertyName}\" are not equals " +
					$"{propertyName}");
			}
			else
			{
				Assert.AreEqual(typeof(NullValidationError), error.GetType(), 
					$"Current error type \"{error.GetType().Name}\" " +
					$"not equals to target error type \"{typeof(NullValidationError).Name}\"");
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