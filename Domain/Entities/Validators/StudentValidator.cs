using System.Threading.Tasks;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.Domain.Entities.Validators
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
			DefaultValidations.DenyNullOrEmptyString(nameof(Firstname), ref value, 40);
		}

		public void Middlename(ref string value)
		{
			DefaultValidations.AllowNullOrEmptyString(nameof(Middlename), ref value, 60);
		}

		public void Lastname(ref string value)
		{
			DefaultValidations.DenyNullOrEmptyString(nameof(Lastname), ref value, 40);
		}

		public void Callsign(ref string value)
		{
			DefaultValidations.AllowNullOrEmptyString(nameof(Callsign), ref value, 6, 16);
		}

		public async Task CallsignUniqueness(string value)
		{
			if (await _studentValidationService.CallsignExistsAsync(value))
				ValidationFail(new UniquenessValidationError<string>(nameof(Callsign), value));
		}

		public void SexId(int sexId)
		{
			const string propertyName = nameof(SexId);
			ClearErrors(propertyName);
			BaseValidations.IsOutOfRange(sexId, 1, 2, propertyName);
		}
	}
}
