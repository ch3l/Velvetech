namespace Velvetech.Domain.Common.Validation.Errors.Base
{
	public abstract class DetailedValidationError<TValue> : ValidationError
	{
		/// <summary>
		/// Not valid value
		/// </summary>
		public TValue Value { get; }

		protected DetailedValidationError(string propertyName, TValue value)
			: base(propertyName)
		{
			Value = value;
		}
	}
}