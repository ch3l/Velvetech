using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Common.Validation.Errors.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Entities
{
	[TestClass]
	public class StudentTests
	{
		private const string ClassName = nameof(Student);

		private const string SexId = nameof(Student.SexId);
		private const int SexIdLowerBoundary = 1;
		private const int SexIdUpperBoundary = 2;

		private const string Firstname = nameof(Student.Firstname);
		private const int FirstnameLengthUpperBoundary = 40;

		private const string Middlename = nameof(Student.Middlename);
		private const int MiddlenameLengthUpperBoundary = 60;

		private const string Lastname = nameof(Student.Lastname);
		private const int LastnameLengthUpperBoundary = 40;

		private const string Callsign = nameof(Student.Callsign);
		private const int CallsignLengthLowerBoundary = 6;
		private const int CallsignLengthUpperBoundary = 16;

		[TestMethod]
		public async Task SetSexIdTestAsync()
		{
			const int lowerCross = SexIdLowerBoundary - 1;
			const int lower = SexIdLowerBoundary;
			const int upper = SexIdUpperBoundary;
			const int upperCross = SexIdUpperBoundary + 1;

			// Lower cross
			{
				var errors = await GetSexIdInitializationErrors(lowerCross);
				EntityTestHelper.CheckForSingleError<ComparisonValidationError<int>>(ClassName, SexId, errors);
			}

			// Lower
			{
				var errors = await GetSexIdInitializationErrors(lower);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, SexId);
			}

			// Upper
			{
				var errors = await GetSexIdInitializationErrors(upper);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, SexId);
			}

			// Upper cross
			{
				var errors = await GetSexIdInitializationErrors(upperCross);
				EntityTestHelper.CheckForSingleError<ComparisonValidationError<int>>(ClassName, SexId, errors);
			}
		}

		[TestMethod]
		public async Task SetFirstnameTestAsync()
		{
			// Null value test
			{
				string value = null;
				var errors = await GetFirstnameInitializationErrors(value);
				EntityTestHelper.CheckForSingleError<NullValidationError>(ClassName, Firstname, errors);
			}

			// Empty value Test
			{
				var value = "";
				var errors = await GetFirstnameInitializationErrors(value);
				EntityTestHelper.CheckForSingleError<EmptyValidationError<IEnumerable<char>>>(ClassName, Firstname, errors);
			}

			// Whitespaces only Test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, FirstnameLengthUpperBoundary).Select(x => ' ').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, FirstnameLengthUpperBoundary + 1).Select(x => ' ').ToArray());

				// Checking upper boundary without crossing
				{
					var errors = await GetFirstnameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Firstname, errors);
				}

				// Checking upper boundary cross
				{
					var errors = await GetFirstnameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Firstname, errors);
				}
			}

			// Upper boundary test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, FirstnameLengthUpperBoundary).Select(x => 'X').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, FirstnameLengthUpperBoundary + 1).Select(x => 'X').ToArray());

				// Checking upper boundary
				{
					var errors = await GetFirstnameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Firstname);
				}

				// Checking upper boundary cross
				{
					var errors = await GetFirstnameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					var error = EntityTestHelper.CheckForSingleError<LengthComparisonValidationError>(ClassName, Firstname, errors);
					EntityTestHelper.CheckUpperBoundaryCross(FirstnameLengthUpperBoundary, error);
				}
			}
		}

		[TestMethod]
		public async Task SetMiddlenameTestAsync()
		{
			// Null value test
			{
				string value = null;
				var errors = await GetMiddlenameInitializationErrors(value);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Middlename);
			}

			// Empty value Test
			{
				var value = "";
				var errors = await GetMiddlenameInitializationErrors(value);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Middlename);
			}

			// Whitespaces only Test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary).Select(x => ' ').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary+ 1).Select(x => ' ').ToArray());

				// Checking upper boundary
				{
					var errors = await GetMiddlenameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Middlename, errors);
				}

				// Checking upper boundary cross
				{
					var errors = await GetMiddlenameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Middlename, errors);
				}
			}

			// Upper boundary test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary).Select(x => 'X').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, MiddlenameLengthUpperBoundary+ 1).Select(x => 'X').ToArray());

				// Checking upper boundary
				{
					var errors = await GetMiddlenameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Middlename);
				}

				// Checking upper boundary cross
				{
					var errors = await GetMiddlenameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					var error = EntityTestHelper.CheckForSingleError<LengthComparisonValidationError>(ClassName, Middlename, errors);
					EntityTestHelper.CheckUpperBoundaryCross(MiddlenameLengthUpperBoundary, error);
				}
			}
		}

		[TestMethod]
		public async Task SetLastnameTestAsync()
		{
			// Null value test
			{
				string value = null;
				var errors = await GetLastnameInitializationErrors(value);
				EntityTestHelper.CheckForSingleError<NullValidationError>(ClassName, Lastname, errors);
			}

			// Empty value Test
			{
				var value = "";
				var errors = await GetLastnameInitializationErrors(value);
				EntityTestHelper.CheckForSingleError<EmptyValidationError<IEnumerable<char>>>(ClassName, Lastname, errors);
			}

			// Whitespaces only Test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, LastnameLengthUpperBoundary).Select(x => ' ').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, LastnameLengthUpperBoundary + 1).Select(x => ' ').ToArray());

				// Checking upper boundary without crossing
				{
					var errors = await GetLastnameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Lastname, errors);
				}

				// Checking upper boundary cross
				{
					var errors = await GetLastnameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Lastname, errors);
				}
			}

			// Upper boundary test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, LastnameLengthUpperBoundary).Select(x => 'X').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, LastnameLengthUpperBoundary + 1).Select(x => 'X').ToArray());

				// Checking upper boundary
				{
					var errors = await GetLastnameInitializationErrors(valueWithUpperBoundaryLength);
					EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Lastname);
				}

				// Checking upper boundary cross
				{
					var errors = await GetLastnameInitializationErrors(valueWithCrossingUpperBoundaryLength);
					var error = EntityTestHelper.CheckForSingleError<LengthComparisonValidationError>(ClassName, Lastname, errors);
					EntityTestHelper.CheckUpperBoundaryCross(LastnameLengthUpperBoundary, error);
				}
			}
		}

		[TestMethod]
		public async Task SetCallsignTestAsync()
		{
			var repository = new FakeStudentRepository();

			// Null value test
			{
				string value = null;
				var errors = await GetCallsignInitializationErrors(repository, value);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Callsign);
			}

			// Empty value Test
			{
				var value = "";
				var errors = await GetCallsignInitializationErrors(repository, value);
				EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Callsign);
			}

			// Whitespaces only Test
			{
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, CallsignLengthUpperBoundary).Select(x => ' ').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, CallsignLengthUpperBoundary + 1).Select(x => ' ').ToArray());

				// Checking upper boundary
				{
					var errors = await GetCallsignInitializationErrors(repository, valueWithUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Callsign, errors);
				}

				// Checking upper boundary cross
				{
					var errors = await GetCallsignInitializationErrors(repository, valueWithCrossingUpperBoundaryLength);
					EntityTestHelper.CheckForSingleError<WhitespacesValidationError>(ClassName, Callsign, errors);
				}
			}

			// Upper boundary test
			{
				var valueWithCrossingLowerBoundaryLength = new string(Enumerable.Range(1, CallsignLengthLowerBoundary - 1).Select(x => 'X').ToArray());
				var valueWithLowerBoundaryLength = new string(Enumerable.Range(1, CallsignLengthLowerBoundary).Select(x => 'X').ToArray());
				var valueWithUpperBoundaryLength = new string(Enumerable.Range(1, CallsignLengthUpperBoundary).Select(x => 'X').ToArray());
				var valueWithCrossingUpperBoundaryLength = new string(Enumerable.Range(1, CallsignLengthUpperBoundary + 1).Select(x => 'X').ToArray());

				// Checking lower boundary
				{
					var errors = await GetCallsignInitializationErrors(repository, valueWithLowerBoundaryLength);
					EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Callsign);
				}

				// Checking lower boundary cross
				{
					var errors = await GetCallsignInitializationErrors(repository, valueWithCrossingLowerBoundaryLength);
					var error = EntityTestHelper.CheckForSingleError<LengthComparisonValidationError>(ClassName, Callsign, errors);
					EntityTestHelper.CheckLowerBoundaryCross(CallsignLengthLowerBoundary, error);
				}

				// Checking upper boundary
				{
					var errors = await GetCallsignInitializationErrors(repository, valueWithUpperBoundaryLength);
					EntityTestHelper.CheckErrorsCount(0, errors, ClassName, Callsign);
				}

				// Checking upper boundary cross
				{
					var errors = await GetCallsignInitializationErrors(repository, valueWithCrossingUpperBoundaryLength);
					var error = EntityTestHelper.CheckForSingleError<LengthComparisonValidationError>(ClassName, Callsign, errors);
					EntityTestHelper.CheckUpperBoundaryCross(CallsignLengthUpperBoundary, error);
				}
			}
		}

		private async Task<ValidationError[]> GetSexIdInitializationErrors(int sexId)
		{
			var validator = new StudentValidator(default);
			var group = await Student.BuildAsync(validator,
				sexId: sexId,
				firstname: default,
				middlename: default,
				lastname: default,
				callsign: default);

			if (group.Errors.TryGetValue(SexId, out var errors))
				return errors;

			return new ValidationError[0];
		}

		private async Task<ValidationError[]> GetFirstnameInitializationErrors(string firstname)
		{
			var validator = new StudentValidator(default);
			var group = await Student.BuildAsync(validator,
				sexId: default,
				firstname: firstname,
				middlename: default,
				lastname: default,
				callsign: default);

			if (group.Errors.TryGetValue(Firstname, out var errors))
				return errors;

			return new ValidationError[0];
		}

		private async Task<ValidationError[]> GetMiddlenameInitializationErrors(string middlename)
		{
			var validator = new StudentValidator(default);
			var group = await Student.BuildAsync(validator,
				sexId: default,
				firstname: default,
				middlename: middlename,
				lastname: default,
				callsign: default);

			if (group.Errors.TryGetValue(Middlename, out var errors))
				return errors;

			return new ValidationError[0];
		}

		private async Task<ValidationError[]> GetLastnameInitializationErrors(string lastname)
		{
			var validator = new StudentValidator(default);
			var group = await Student.BuildAsync(validator,
				sexId: default,
				firstname: default,
				middlename: default,
				lastname: lastname,
				callsign: default);

			if (group.Errors.TryGetValue(Lastname, out var errors))
				return errors;

			return new ValidationError[0];
		}

		private async Task<ValidationError[]> GetCallsignInitializationErrors(
			IAsyncRepository<Student, Guid> repository, 
			string callsign)
		{
			var validator = new StudentValidator(new StudentValidationService(repository));
			var group = await Student.BuildAsync(validator,
				sexId: default,
				firstname: default,
				middlename: default,
				lastname: default,
				callsign: callsign);

			if (group.Errors.TryGetValue(Callsign, out var errors))
				return errors;

			return new ValidationError[0];
		}

		[TestMethod]
		public void ExcludeFromAllGroupsTest()
		{
			Assert.Fail();
		}
	}
}