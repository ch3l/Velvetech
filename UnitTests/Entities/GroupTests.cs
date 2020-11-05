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
		private const string Name = nameof(Group.Name);
		private const int NameLengthLengthUpperBoundary = 25;

		#region Properties validation

		[TestMethod]
		public void SetNameTest()
		{
			// Null value test
			{
				string value = null;
				var errors = GetNameInitializationErrors(value);
				CheckForOnlyOneError<NullValidationError>(Name, errors);
			}

			// Empty value Test
			{
				string value = "";
				var errors = GetNameInitializationErrors(value);
				CheckForOnlyOneError<EmptyValidationError<IEnumerable<char>>>(Name, errors);
			}

			// Whitespaces only Test
			{
				var upperBoundaryValue = new string(Enumerable.Range(1, NameLengthLengthUpperBoundary).Select(x => ' ').ToArray());
				var longerValue = new string(Enumerable.Range(1, NameLengthLengthUpperBoundary + 1).Select(x => ' ').ToArray());

				// Checking upper boundary without crossing
				{
					var errors = GetNameInitializationErrors(upperBoundaryValue);
					CheckForOnlyOneError<WhitespacesValidationError>(Name, errors);
				}

				// Checking upper boundary cross
				{
					var errors = GetNameInitializationErrors(longerValue);
					CheckForOnlyOneError<WhitespacesValidationError>(Name, errors);
				}
			}

			// Upper boundary test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, NameLengthLengthUpperBoundary).Select(x => 'X').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, NameLengthLengthUpperBoundary + 1).Select(x => 'X').ToArray());

				// Checking upper boundary without crossing
				{
					var errors = GetNameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Name);
				}

				// Checking upper boundary cross
				{
					var errors = GetNameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					var error = CheckForOnlyOneError<LengthComparisonValidationError>(Name, errors);
					EntityTestHelper.CheckUpperBoundaryCross(NameLengthLengthUpperBoundary, error);
				}
			}
		}

		private TTargetValidationError CheckForOnlyOneError<TTargetValidationError>(string propertyName, ValidationError[] errors)
			where TTargetValidationError : ValidationError
		{
			EntityTestHelper.CheckErrorsCount(1, errors, ClassName, propertyName);

			var error = errors[0];
			EntityTestHelper.CheckErrorType<TTargetValidationError>(error);

			return (TTargetValidationError)error;
		}

		private ValidationError[] GetNameInitializationErrors(string value)
		{
			var validator = new GroupValidator();
			var group = Group.Build(validator, value);

			if (group.Errors.TryGetValue(Name, out var errors))
				return errors;

			return new ValidationError[0];
		}

		#endregion

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
	}
}