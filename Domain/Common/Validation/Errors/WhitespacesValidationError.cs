using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	public class WhitespacesValidationError : DetailedValidationError<string>
	{
		public WhitespacesValidationError(string propertyName, string value)
			: base(propertyName, value)
		{
		}

		public override string ToString()
		{
			return "Consists of whitespaces only";
		}
	}
}