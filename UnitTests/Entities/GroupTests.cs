using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.UnitTests.Entities.Builders;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class GroupTests
	{
		private const string ClassName = nameof(Group);
		private const string PropertyName = nameof(Group.Name);
		private const int UpperLengthBoundary = 25;

		[TestMethod]
		public void SetNameTest()
		{
			CheckSetName<NullValidationError>(null);
		}

		[TestMethod]
		public void SetNameTestIsEmpty()
		{
			CheckSetName<EmptyValidationError<IEnumerable<char>>>("");
		}

		[TestMethod]
		public void SetNameTestForWhitespacesAndBoundary()
		{
			var upperBoundaryValue = new string(Enumerable.Range(1, UpperLengthBoundary).Select(x => ' ').ToArray());
			var longerValue = new string(Enumerable.Range(1, UpperLengthBoundary + 1).Select(x => ' ').ToArray());

			// Checking upper boundary without crossing
			CheckSetName<WhitespacesValidationError>(upperBoundaryValue);

			// Checking upper boundary cross
			CheckSetName<WhitespacesValidationError>(longerValue);
		}

		[TestMethod]
		public void SetNameTestLengthUpperBoundary()
		{
			var boundaryValue = new string(Enumerable.Range(1, UpperLengthBoundary).Select(x => 'X').ToArray());
			var longerValue = new string(Enumerable.Range(1, UpperLengthBoundary + 1).Select(x => 'X').ToArray());

			// Checking upper boundary without crossing
			{
				var errors = GetErrorsOfSetName(boundaryValue);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, PropertyName);
			}

			// Checking upper boundary cross
			{
				var errors = GetErrorsOfSetName(longerValue);
				EntityTestHelper.CheckErrorsCount(1, errors, ClassName, PropertyName);

				var error = errors[0];
				EntityTestHelper.CheckErrorType<LengthComparisonValidationError>(error);

				var lengthComparisonError = (LengthComparisonValidationError)error;
				Assert.AreEqual(25, lengthComparisonError.ComparisonValue);
				Assert.AreEqual(ComparisonResultType.More, lengthComparisonError.ComparisonResult);
			}
		}

		[TestMethod]
		public async Task IncludeStudentTestAsync()
		{
			var groupRepository = new FakeGroupRepository();
			var group = await new GroupBuilder().Build(groupRepository, 1);

			var studentRepository = new FakeStudentRepository();
			var student1 = await new StudentBuilder().Build(studentRepository, 1);
			var student2 = await new StudentBuilder().Build(studentRepository, 2);

			Assert.AreEqual(true, group.IncludeStudent(student1));
			Assert.AreEqual(1, group.Grouping.Count);

			Assert.AreEqual(true, group.IncludeStudent(student2));
			Assert.AreEqual(2, group.Grouping.Count);

			Assert.AreEqual(false, group.IncludeStudent(student1));
			Assert.AreEqual(2, group.Grouping.Count);
			
			Assert.AreEqual(false, group.IncludeStudent(student2));
			Assert.AreEqual(2, group.Grouping.Count);
		}

		[TestMethod]
		public async Task ExcludeStudentTestAsync()
		{
			var groupRepository = new FakeGroupRepository();
			var group = await new GroupBuilder().Build(groupRepository, 1);

			var studentRepository = new FakeStudentRepository();
			var student1 = await new StudentBuilder().Build(studentRepository, 1);
			var student2 = await new StudentBuilder().Build(studentRepository, 2);

			group.IncludeStudent(student1);
			group.IncludeStudent(student2);

			Assert.AreEqual(true, group.ExcludeStudent(student1));
			Assert.AreEqual(1, group.Grouping.Count);
			Assert.AreEqual(false, group.ExcludeStudent(student1));
			Assert.AreEqual(1, group.Grouping.Count);

			Assert.AreEqual(true, group.ExcludeStudent(student2));
			Assert.AreEqual(0, group.Grouping.Count);
			Assert.AreEqual(false, group.ExcludeStudent(student2));
			Assert.AreEqual(0, group.Grouping.Count);
		}

		[TestMethod]
		public async Task ExcludeAllStudentsTestAsync()
		{
			var groupRepository = new FakeGroupRepository();
			var group = await new GroupBuilder().Build(groupRepository, 1);

			var studentRepository = new FakeStudentRepository();
			var student1 = await new StudentBuilder().Build(studentRepository, 1);
			var student2 = await new StudentBuilder().Build(studentRepository, 2);

			group.IncludeStudent(student1);
			Assert.AreEqual(true, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);

			group.IncludeStudent(student2);
			Assert.AreEqual(true, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);

			group.IncludeStudent(student1);
			group.IncludeStudent(student2);

			group.IncludeStudent(student2);
			Assert.AreEqual(true, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);

			Assert.AreEqual(false, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);
		}

		void CheckSetName<TTargetValidationError>(string value)
			where TTargetValidationError : ValidationError
		{
			var errors = GetErrorsOfSetName(value);
			EntityTestHelper.CheckErrorsCount(1, errors, ClassName, PropertyName);

			var error = errors[0];
			EntityTestHelper.CheckErrorType<TTargetValidationError>(error);
		}

		private ValidationError[] GetErrorsOfSetName(string value)
		{
			var validator = new GroupValidator();
			var group = Group.Build(validator, value);

			if (group.Errors.TryGetValue(PropertyName, out var errors))
				return errors;

			return new ValidationError[0];
		}
	}
}