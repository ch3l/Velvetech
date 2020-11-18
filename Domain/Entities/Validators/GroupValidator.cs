using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validators
{
	public abstract class GroupValidator : EntityValidator
	{
		internal abstract void Name(ref string value);
	}

	public class DefaultGroupValidator: GroupValidator
	{
		internal override void Name(ref string value)
		{
			DefaultValidations.DenyNullOrEmptyString(nameof(Name), ref value, 25);
		}
	}
}