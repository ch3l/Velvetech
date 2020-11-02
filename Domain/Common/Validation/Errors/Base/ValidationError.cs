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
	}
}
