using System;

namespace Velvetech.Domain.Common.Validation.Errors.Base
{
	public abstract class ValidationError
	{
		protected ValidationError(string propertyName)
		{
			PropertyName = propertyName;
		}

		/// <summary>
		/// Target property propertyName
		/// </summary>
		public string PropertyName { get; }

		public abstract override string ToString();

		public override int GetHashCode()
		{
			return HashCode.Combine(GetType(), PropertyName);
		}

		public override bool Equals(object obj)
		{
			return obj is ValidationError validationError
			       && validationError.GetType() == this.GetType()
			       && validationError.PropertyName == PropertyName;
		}
	}
}
