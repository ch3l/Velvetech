using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal;
using Velvetech.UnitTests.Helpers;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities.Validators
{
	[TestClass]
	public class StudentValidatorTests
	{
		private const int SexIdMin = 1;
		private const int SexIdMax = 2;

		private const int FirstnameUpperBoundary = 40;

		private const int MiddlenameUpperBoundary = 60;

		private const int LastnameUpperBoundary = 40;

		private const int CallsignLowerBoundary = 6;
		private const int CallsignUpperBoundary = 16;

		[TestMethod]
		public void SexIdTest()
		{
			const string propertyName = nameof(StudentValidator.SexId);
			const int min = SexIdMin;
			const int max = SexIdMax;

			var repository = new FakeStudentRepository();

			// Check less than min
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = min - 1;
				validator.SexId(value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				var error = ValidationsTestHelper.CheckErrorType<ComparisonValidationError<int>>(errors);
				Assert.AreEqual(ComparisonResultType.Less, error.ComparisonResult);
				Assert.AreEqual(min, error.ComparisonValue);
			}

			// Check is min
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = min;
				validator.SexId(value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Check is max
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = max;
				validator.SexId(value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Check bigger than max
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = max + 1;
				validator.SexId(value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				var error = ValidationsTestHelper.CheckErrorType<ComparisonValidationError<int>>(errors);
				Assert.AreEqual(ComparisonResultType.More, error.ComparisonResult);
				Assert.AreEqual(max, error.ComparisonValue);
			}
		}

		[TestMethod]
		public void FirstnameTest()
		{
			const string propertyName = nameof(StudentValidator.Firstname);
			const int upperBoundary = FirstnameUpperBoundary;

			var repository = new FakeStudentRepository();

			// Check for null
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				string value = null;
				validator.Firstname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<NullValidationError>(errors);
			}

			// Check is empty
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = "";
				validator.Firstname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<EmptyValidationError<IEnumerable<char>>>(errors);
			}

			// Whitespaces check without boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => ' ').ToArray());
				validator.Firstname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Whitespaces check with boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => ' ').ToArray());
				validator.Firstname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Upper boundary check without crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => 'x').ToArray());
				validator.Firstname(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Upper boundary check with crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => 'x').ToArray());
				validator.Firstname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<LengthComparisonValidationError>(errors);
				ValidationsTestHelper.CheckUpperBoundaryCross(
					(LengthComparisonValidationError)validator.Errors[propertyName][0],
					upperBoundary);
			}
		}

		[TestMethod]
		public void MiddlenameTest()
		{
			const string propertyName = nameof(StudentValidator.Middlename);
			const int upperBoundary = MiddlenameUpperBoundary;

			var repository = new FakeStudentRepository();

			// Check for null
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				string value = null;
				validator.Middlename(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Check is empty
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = "";
				validator.Middlename(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Whitespaces check without boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => ' ').ToArray());
				validator.Middlename(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Whitespaces check with boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => ' ').ToArray());
				validator.Middlename(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Upper boundary check without crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => 'x').ToArray());
				validator.Middlename(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Upper boundary check with crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => 'x').ToArray());
				validator.Middlename(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<LengthComparisonValidationError>(errors);
				ValidationsTestHelper.CheckUpperBoundaryCross(
					(LengthComparisonValidationError)validator.Errors[propertyName][0],
					upperBoundary);
			}
		}

		[TestMethod]
		public void LastnameTest()
		{
			const string propertyName = nameof(StudentValidator.Lastname);
			const int upperBoundary = LastnameUpperBoundary;

			var repository = new FakeStudentRepository();

			// Check for null
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				string value = null;
				validator.Lastname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<NullValidationError>(errors);
			}

			// Check for null
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = "";
				validator.Lastname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<EmptyValidationError<IEnumerable<char>>>(errors);
			}

			// Whitespaces check without boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => ' ').ToArray());
				validator.Lastname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Whitespaces check with boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => ' ').ToArray());
				validator.Lastname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Upper boundary check without crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => 'x').ToArray());
				validator.Lastname(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Upper boundary check with crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => 'x').ToArray());
				validator.Lastname(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<LengthComparisonValidationError>(errors);
				ValidationsTestHelper.CheckUpperBoundaryCross(
					(LengthComparisonValidationError)validator.Errors[propertyName][0],
					upperBoundary);
			}
		}

		[TestMethod]
		public async Task CallsignTestAsync()
		{
			const string propertyName = nameof(StudentValidator.Callsign);
			const int lowerBoundary = CallsignLowerBoundary;
			const int upperBoundary = CallsignUpperBoundary;

			var repository = new FakeStudentRepository();

			// Check for null
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				string value = null;
				validator.Callsign(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Check is empty
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = "";
				validator.Callsign(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Whitespaces check without boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => ' ').ToArray());
				validator.Callsign(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Whitespaces check with boundary crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => ' ').ToArray());
				validator.Callsign(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Lower boundary check without crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, lowerBoundary).Select(x => 'x').ToArray());
				validator.Callsign(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Lower boundary check with crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, lowerBoundary - 1).Select(x => 'x').ToArray());
				validator.Callsign(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<LengthComparisonValidationError>(errors);
				ValidationsTestHelper.CheckLowerBoundaryCross(
					(LengthComparisonValidationError)validator.Errors[propertyName][0],
					lowerBoundary);
			}

			// Upper boundary check without crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary).Select(x => 'x').ToArray());
				validator.Callsign(ref value);

				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Upper boundary check with crossing
			{
				var validator = new StudentValidator(new StudentValidationService(repository));
				var value = new string(Enumerable.Range(1, upperBoundary + 1).Select(x => 'x').ToArray());
				validator.Callsign(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<LengthComparisonValidationError>(errors);
				ValidationsTestHelper.CheckUpperBoundaryCross(
					(LengthComparisonValidationError)validator.Errors[propertyName][0],
					upperBoundary);
			}

			// Check for uniqueness
			{
				var validator = new StudentValidator(new StudentValidationService(repository));

				string value = null;
				await validator.CallsignUniqueness(value);
				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);

				value = new string(Enumerable.Range(1, lowerBoundary).Select(x => 'x').ToArray());
				await validator.CallsignUniqueness(value);
				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);

				var student = await Student.BuildAsync(validator, 1, "firstname", "middlename", "lastname", value);
				await repository.AddAsync(student);
				await validator.CallsignUniqueness(value);
				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<UniquenessValidationError<string>>(errors);

				value = null;
				await validator.CallsignUniqueness(value);
				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<UniquenessValidationError<string>>(errors);
			}
		}
	}
}