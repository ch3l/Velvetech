using System;

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

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), Value);
		}

		public override bool Equals(object obj)
		{
			return obj is DetailedValidationError<TValue> validationError
			       && validationError.GetType() == this.GetType()
			       && validationError.PropertyName == PropertyName
			       && validationError.Value.Equals(Value);
		}
	}
}