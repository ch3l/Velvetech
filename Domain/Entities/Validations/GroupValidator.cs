using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validations
{
	public class GroupValidator: Validator
	{
		public void Name(ref string value)
		{
			var propertyName = nameof(Name);

			if (IsNull(value, propertyName))
				return;

			if (IsEmpty(value, propertyName))
				return;

			if (IsWhitespaces(value, propertyName))
				return;

			value = value.Trim();
			IsLongerThan(value, 25, propertyName);
		}
	}
}