using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	public class EmptyValidationError<TValue> : DetailedValidationError<TValue>
	{
		public EmptyValidationError(string propertyName, TValue value)
			: base(propertyName, value)
		{
		}

		public override string ToString()
		{
			return "Is empty";
		}
	}
}