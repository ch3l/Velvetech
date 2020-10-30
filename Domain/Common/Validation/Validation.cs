namespace Velvetech.Domain.Common.Validation
{
	public abstract class Validation<TEntity, TKey> where TEntity : ValidatableEntity<TKey>
	{
		protected Validation(TEntity entity)
		{
			Validate(entity);
		}

		protected abstract void Validate(TEntity entity);
	}

	public static class ValidationRule
	{
		public static bool IsNull<TKey, TIn>(this ValidationEntryPoint<TKey> entryPoint, TIn value, string propertyName)
		{
			if (value is null)
			{
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName} is null");
				return true;
			}

			return false;
		}

		public static void IsEmpty<TKey>(this ValidationEntryPoint<TKey> entryPoint, string value, string propertyName)
		{
			if (value == string.Empty)
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName} is empty");
		}

		public static void IsWhiteSpaces<TKey>(this ValidationEntryPoint<TKey> entryPoint, string value, string propertyName)
		{
			if (value.Trim() == string.Empty)
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName}'s value \"{value}\" consists of whitespaces only");
		}

		public static void IsLengthOver<TKey>(this ValidationEntryPoint<TKey> entryPoint, string value, int maxLength, string propertyName)
		{
			if (value.Length > maxLength)
				entryPoint.Entity.ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is over {maxLength}");
		}

		public static void IsLengthLess<TKey>(this ValidationEntryPoint<TKey> entryPoint, string value, int minLength, string propertyName)
		{
			if (value.Length < minLength)
				entryPoint.Entity.ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is less than {minLength}");
		}
	}
}
