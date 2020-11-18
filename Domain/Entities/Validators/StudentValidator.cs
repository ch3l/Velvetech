using System.Threading.Tasks;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Errors;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.Domain.Entities.Validators
{
	public abstract class StudentValidator : EntityValidator
	{
		internal abstract void Firstname(ref string value);

		internal abstract void Middlename(ref string value);

		internal abstract void Lastname(ref string value);

		internal abstract void Callsign(ref string value);

		internal abstract Task CallsignUniqueness(string value);

		internal abstract void SexId(int sexId);
	}

	public class DefaultStudentValidator : StudentValidator
	{
		private readonly IStudentValidationService _studentValidationService;

		public DefaultStudentValidator(IStudentValidationService studentValidationService)
		{
			_studentValidationService = studentValidationService;
		}

		internal override void Firstname(ref string value)
		{
			DefaultValidations.DenyNullOrEmptyString(nameof(Firstname), ref value, 40);
		}

		internal override void Middlename(ref string value)
		{
			DefaultValidations.AllowNullOrEmptyString(nameof(Middlename), ref value, 60);
		}

		internal override void Lastname(ref string value)
		{
			DefaultValidations.DenyNullOrEmptyString(nameof(Lastname), ref value, 40);
		}

		internal override void Callsign(ref string value)
		{
			DefaultValidations.AllowNullOrEmptyString(nameof(Callsign), ref value, 6, 16);
		}

		internal override async Task CallsignUniqueness(string value)
		{
			if (await _studentValidationService.CallsignExistsAsync(value))
				ValidationFail(new UniquenessValidationError<string>(nameof(Callsign), value));
		}

		internal override void SexId(int sexId)
		{
			const string propertyName = nameof(SexId);
			ClearErrors(propertyName);
			BaseValidations.IsOutOfRange(sexId, 1, 2, propertyName);
		}
	}
}
