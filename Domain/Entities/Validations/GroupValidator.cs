using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validations
{
	public class GroupValidator: Validator
	{
		public void Name(string value, string propertyName)
		{
			if (IsNull(value, propertyName))
				return;

			IsEmpty(value, propertyName);
			IsWhitespaces(value, propertyName);
			IsExceedSize(value, 25, propertyName);
		}
	}
}