using System.Threading.Tasks;

using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.Domain.Entities.Validations
{
	public class StudentValidator : EntityValidator
	{
		private readonly IStudentValidationService _studentValidationService;

		public StudentValidator(IStudentValidationService studentValidationService)
		{
			_studentValidationService = studentValidationService;
		}

		public void Firstname(ref string value)
		{
			const string propertyName = nameof(Firstname);

			if (IsNull(value, propertyName))
				return;

			if (IsEmpty(value, propertyName))
				return;

			if (IsWhitespaces(value, propertyName))
				return;

			value = value.Trim();
			IsLongerThan(value, 40, propertyName);
		}

		public void Middlename(ref string value)
		{
			const string propertyName = nameof(Middlename);

			if (value is null)
				return;

			if (EmptyAsNull(ref value))
				return;

			if (IsWhitespaces(value, propertyName))
				return;

			value = value.Trim();
			IsLongerThan(value, 60, propertyName);
		}

		public void Lastname(ref string value)
		{
			const string propertyName = nameof(Lastname);

			if (IsNull(value, propertyName))
				return;

			if (IsEmpty(value, propertyName))
				return;

			if (IsWhitespaces(value, propertyName))
				return;

			value = value.Trim();
			IsLongerThan(value, 40, propertyName);
		}

		public void Callsign(ref string value)
		{
			const string propertyName = nameof(Callsign);

			if (value is null)
				return;

			if (EmptyAsNull(ref value))
				return;

			if (IsWhitespaces(value, propertyName))
				return;
			
			value = value.Trim();
			IsLengthOutOfRange(value, 6, 16, propertyName);
		}

		public async Task CallsignUniqueness(string value)
		{
			if (await _studentValidationService.CallsignExistsAsync(value))
				ValidationFail(new UniquenessError(nameof(Callsign)));
		}

		public void SexId(int sexId)
		{
			IsOutOfRange(sexId, 1, 2, nameof(SexId));
		}
	}
}
