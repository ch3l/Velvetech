using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal;
using Velvetech.UnitTests.Entities.Builders;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class StudentTests
	{

		private const int SexIdMin = 1;
		private const int SexIdMax = 2;
		private int ValidSexId => SexIdMax;
		private int NotValidSexId => SexIdMax + 1;

		private const int FirstnameLengthUpperBoundary = 40;
		private string ValidFirstname =>
			new string(Enumerable.Range(1, FirstnameLengthUpperBoundary).Select(x => 'f').ToArray());
		private string NotValidFirstname =>
			new string(Enumerable.Range(1, FirstnameLengthUpperBoundary + 1).Select(x => 'f').ToArray());

		private const int MiddlenameLengthUpperBoundary = 60;
		private string ValidMiddlename =>
			new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary).Select(x => 'm').ToArray());
		private string NotValidMiddlename =>
			new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary + 1).Select(x => 'm').ToArray());

		private const int LastnameLengthUpperBoundary = 40;
		private string ValidLastname =>
			new string(Enumerable.Range(1, LastnameLengthUpperBoundary).Select(x => 'l').ToArray());
		private string NotValidLastname =>
			new string(Enumerable.Range(1, LastnameLengthUpperBoundary + 1).Select(x => 'l').ToArray());

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
			Assert.Fail("Has to be in integration tests");

			var groupRepository = new FakeGroupRepository();
			var studentRepository = new FakeStudentRepository();

			var group1 = await GroupBuilder.BuildAsync(groupRepository, 1);
			var group2 = await GroupBuilder.BuildAsync(groupRepository, 2);

			var student = await StudentBuilder.BuildAsync(studentRepository, 1);

			group1.IncludeStudent(student);
			group2.IncludeStudent(student);

			Assert.AreEqual(true, student.ExcludeFromAllGroups());
			Assert.AreEqual(0, group1.Grouping.Count);
			Assert.AreEqual(0, group2.Grouping.Count);
		}
	}
}