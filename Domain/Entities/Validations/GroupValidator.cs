using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validations
{
	public class GroupValidator: Validator
	{
		public void Name(string value, string propertyName)
		{
			if (IsNull(value, propertyName))
				return;

			if (IsEmpty(value, propertyName))
				return;

			IsWhitespaces(value, propertyName);
			IsMoreThanLength(value, 25, propertyName);
		}
	}
}