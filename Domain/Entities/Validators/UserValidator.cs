using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validators
{
	public class RoleValidator: EntityValidator
	{
		public void Name(ref string value)
		{
			DefaultValidations.DenyNullOrEmptyString(nameof(Name), ref value, 40);
		}
	}
}