using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Entities.Validations;
using Velvetech.UnitTests.Helpers;

namespace Velvetech.UnitTests.Entities.Validations
{
	[TestClass]
	public class GroupValidatorTests
	{
		private const int NameUpperBoundary = 25;

		[TestMethod]
		public void NameTest()
		{
			const string propertyName = nameof(GroupValidator.Name);

			// Check for null
			{
				var validator = new GroupValidator();
				string value = null;
				validator.Name(ref value);
				
				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<NullValidationError>(errors);
			}

			// Check for null
			{
				var validator = new GroupValidator();
				var value = "";
				validator.Name(ref value);
				
				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<EmptyValidationError<IEnumerable<char>>>(errors);
			}

			// Whitespaces check without boundary crossing
			{
				var validator = new GroupValidator();
				var value = new string(Enumerable.Range(1, NameUpperBoundary).Select(x => ' ').ToArray());
				validator.Name(ref value);
				
				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Whitespaces check with boundary crossing
			{
				var validator = new GroupValidator();
				var value = new string(Enumerable.Range(1, NameUpperBoundary + 1).Select(x => ' ').ToArray());
				validator.Name(ref value);
				
				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<WhitespacesValidationError>(errors);
			}

			// Upper boundary check without crossing
			{
				var validator = new GroupValidator();
				var value = new string(Enumerable.Range(1, NameUpperBoundary).Select(x => 'x').ToArray());
				validator.Name(ref value);
				
				ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 0);
			}

			// Upper boundary check with crossing
			{
				var validator = new GroupValidator();
				var value = new string(Enumerable.Range(1, NameUpperBoundary + 1).Select(x => 'x').ToArray());
				validator.Name(ref value);

				var errors = ValidationsTestHelper.CheckErrorsCount(validator, propertyName, 1);
				ValidationsTestHelper.CheckErrorType<LengthComparisonValidationError>(errors);
				ValidationsTestHelper.CheckUpperBoundaryCross(
					(LengthComparisonValidationError)validator.Errors[propertyName][0], 
					NameUpperBoundary);
			}
		}
	}
}