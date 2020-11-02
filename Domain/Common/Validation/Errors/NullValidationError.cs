using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	public class NullValidationError: ValidationError
	{
		public NullValidationError(string propertyName) 
			: base(propertyName)
		{
		}

		public override string ToString()
		{
			return "Is null";
		}
	}
}