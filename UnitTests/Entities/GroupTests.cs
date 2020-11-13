using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.UnitTests.Entities.Builders;
using Velvetech.UnitTests.Helpers;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class GroupTests
	{
		private const string ClassName = nameof(Group);

		private const string Name = nameof(Group.Name);
		private const int NameLengthUpperBoundary = 25;


		[TestMethod]
		public void SetNameTest()
		{
			// No valid value 
			{
				var value = new string(Enumerable.Range(1, NameLengthUpperBoundary + 1).Select(x => 'x').ToArray());
				var validator = new GroupValidator();
				var group = Group.Build(validator, value);
				Assert.AreEqual(null, group.Name);
			}

			// Valid value 
			{
				var value = "ValidGroupName";
				var validator = new GroupValidator();
				var group = Group.Build(validator, value);
				Assert.AreEqual(value, group.Name);
			}
		}
						  
		[TestMethod]
		public async Task IncludeStudentTestAsync()
		{
			var groupRepository = new FakeGroupRepository();
			var group = await GroupBuilder.BuildAsync(groupRepository, 1);

			var studentRepository = new FakeStudentRepository();
			var student1 = await StudentBuilder.BuildAsync(studentRepository, 1);
			var student2 = await StudentBuilder.BuildAsync(studentRepository, 2);

			Assert.AreEqual(true, group.IncludeStudent(student1));
			Assert.AreEqual(1, group.Grouping.Count);
			Assert.AreEqual(false, group.IncludeStudent(student1));
			Assert.AreEqual(1, group.Grouping.Count);

			Assert.AreEqual(true, group.IncludeStudent(student2));
			Assert.AreEqual(2, group.Grouping.Count);
			Assert.AreEqual(false, group.IncludeStudent(student2));
			Assert.AreEqual(2, group.Grouping.Count);
		}

		[TestMethod]
		public async Task ExcludeStudentTestAsync()
		{
			var groupRepository = new FakeGroupRepository();
			var group = await GroupBuilder.BuildAsync(groupRepository, 1);

			var studentRepository = new FakeStudentRepository();
			var student1 = await StudentBuilder.BuildAsync(studentRepository, 1);
			var student2 = await StudentBuilder.BuildAsync(studentRepository, 2);

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
			var group = await GroupBuilder.BuildAsync(groupRepository, 1);

			var studentRepository = new FakeStudentRepository();
			var student1 = await StudentBuilder.BuildAsync(studentRepository, 1);
			var student2 = await StudentBuilder.BuildAsync(studentRepository, 2);

			group.IncludeStudent(student1);
			Assert.AreEqual(true, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);

			group.IncludeStudent(student2);
			Assert.AreEqual(true, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);

			group.IncludeStudent(student1);
			group.IncludeStudent(student2);
			Assert.AreEqual(true, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);
			Assert.AreEqual(false, group.ExcludeAllStudents());
			Assert.AreEqual(0, group.Grouping.Count);
		}
	}
}