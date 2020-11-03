using Microsoft.VisualStudio.TestTools.UnitTesting;
using Velvetech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Entities.Validations;

namespace Velvetech.Domain.Entities.Tests
{
	[TestClass()]
	public class GroupTests
	{
		[TestMethod()]
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

			Assert.AreEqual(group.Errors.ContainsKey(propertyName), true, 
				$"{className}'s property \"{propertyName}\" not found in errors list");

			Assert.AreEqual(group.Errors[propertyName].Length, targetErrorsCount, 
				$"Errors count after validation {className}'s property \"{propertyName}\" are not equal {targetErrorsCount}");

			if (group.Errors[propertyName][0] is NullValidationError)
			{

			}

			Assert.Fail();
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