using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal;
using Velvetech.UnitTests.Helpers;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class StudentTests
	{
		private const string ClassName = nameof(Student);

		private const string SexId = nameof(Student.SexId);
		private const int SexIdMin = 1;
		private const int SexIdMax = 2;
		private int ValidSexId => SexIdMax;
		private int NotValidSexId => SexIdMax+1;

		private const string Firstname = nameof(Student.Firstname);
		private const int FirstnameLengthUpperBoundary = 40;
		private string ValidFirstname => 
			new string(Enumerable.Range(1, FirstnameLengthUpperBoundary).Select(x => 'f').ToArray());
		private string NotValidFirstname =>
			new string(Enumerable.Range(1, FirstnameLengthUpperBoundary+1).Select(x => 'f').ToArray());

		private const string Middlename = nameof(Student.Middlename);
		private const int MiddlenameLengthUpperBoundary = 60;
		private string ValidMiddlename =>
			new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary).Select(x => 'm').ToArray());
		private string NotValidMiddlename =>
			new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary + 1).Select(x => 'm').ToArray());

		private const string Lastname = nameof(Student.Lastname);
		private const int LastnameLengthUpperBoundary = 40;
		private string ValidLastname =>
			new string(Enumerable.Range(1, LastnameLengthUpperBoundary).Select(x => 'l').ToArray());
		private string NotValidLastname =>
			new string(Enumerable.Range(1, LastnameLengthUpperBoundary + 1).Select(x => 'l').ToArray());

		private const string Callsign = nameof(Student.Callsign);
		private const int CallsignLengthLowerBoundary = 6;
		private const int CallsignLengthUpperBoundary = 16;
		private string ValidCallsign =>
			new string(Enumerable.Range(1, CallsignLengthUpperBoundary).Select(x => 'c').ToArray());
		private string NotValidCallsign =>
			new string(Enumerable.Range(1, CallsignLengthUpperBoundary + 1).Select(x => 'c').ToArray());

		[TestMethod]
		public async Task SetSexIdTestAsync()
		{
			// No valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator, 
					NotValidSexId, 
					ValidFirstname, 
					ValidMiddlename, 
					ValidLastname, 
					ValidCallsign);

				Assert.AreEqual(0, student.SexId);
			}

			// Valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(ValidSexId, student.SexId);
			}
		}

		[TestMethod]
		public async Task SetFirstnameTestAsync()
		{
			// No valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					NotValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Firstname);
			}

			// Valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(ValidFirstname, student.Firstname);
			}
		}

		[TestMethod]
		public async Task SetMiddlenameTestAsync()
		{
			// No valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					NotValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Middlename);
			}

			// Valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(ValidMiddlename, student.Middlename);
			}

			// Valid null value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					null,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Middlename);
			}

			// Valid empty value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					string.Empty,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Middlename);
			}
		}

		[TestMethod]
		public async Task SetLastnameTestAsync()
		{
			// No valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					NotValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Firstname);
			}

			// Valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(ValidFirstname, student.Firstname);
			}
		}

		[TestMethod]
		public async Task SetCallsignTestAsync()
		{
			// No valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					NotValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Middlename);
			}

			// Valid value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					ValidMiddlename,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(ValidMiddlename, student.Middlename);
			}

			// Valid null value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					null,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Middlename);
			}

			// Valid empty value 
			{
				var repository = new FakeStudentRepository();
				var validator = new StudentValidator(new StudentValidationService(repository));
				var student = await Student.BuildAsync(validator,
					ValidSexId,
					ValidFirstname,
					string.Empty,
					ValidLastname,
					ValidCallsign);

				Assert.AreEqual(null, student.Middlename);
			}
		}

		[TestMethod]
		public async Task ExcludeFromAllGroupsTestAsync()
		{
			var groupRepository = new FakeGroupRepository();
			var groupValidator = new GroupValidator();

			var studentRepository = new FakeStudentRepository();
			var studentValidator = new StudentValidator(new StudentValidationService(studentRepository));

			var group1 = Group.Build(groupValidator, "Group1");
			var group2 = Group.Build(groupValidator, "Group2");
			
			var student1 = await Student.BuildAsync(studentValidator,
				ValidSexId, 
				ValidFirstname,
				ValidMiddlename,
				ValidLastname,
				"Callsign1");
			
			var student2 = await Student.BuildAsync(studentValidator,
				ValidSexId,
				ValidFirstname,
				ValidMiddlename,
				ValidLastname,
				"Callsign2");
					  
			//group1 = await groupRepository.AddAsync(group1);
			//group2 = await groupRepository.AddAsync(group2);
			//student1 = await studentRepository.AddAsync(student1);
			//student2 = await studentRepository.AddAsync(student2);

			Assert.AreEqual(0, group1.Grouping.Count);
			Assert.AreEqual(0, group2.Grouping.Count);

			var student1ToGroup1IncludeResult = group1.IncludeStudent(student1);
			Assert.AreEqual(true, student1ToGroup1IncludeResult);
			Assert.AreEqual(1, group1.Grouping.Count);
			Assert.AreEqual(student1.Id, group1.Grouping.ToArray()[0].StudentId);
			
			var student2ToGroup1IncludeResult = group1.IncludeStudent(student2);
			Assert.AreEqual(true, student2ToGroup1IncludeResult);
			Assert.AreEqual(2, group1.Grouping.Count);
			Assert.AreEqual(student2.Id, group1.Grouping.ToArray()[1].StudentId);

			//Assert.AreEqual(0, group1.Grouping.Count);
		}
	}
}