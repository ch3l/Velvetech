using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validations
{
	public class GroupValidator: EntityValidator
	{
		public void Name(ref string value)
		{
			DefaultValidations.DenyNullOrEmptyString(nameof(Name), ref value, 25);
		}
	}
}