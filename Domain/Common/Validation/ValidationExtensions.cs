using System;

namespace Velvetech.Domain.Common.Validation
{
	public static class ValidationExtensions
	{
		public static bool IsNull<TEntity, TKey, TIn>(this ValidationEntryPoint<TEntity, TKey> entryPoint, TIn value, string propertyName)
			where TEntity : Entity<TKey>
		{
			if (value is null)
			{
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName} is null");
				return true;
			}

			return false;
		}

		public static void IsEmpty<TEntity, TKey>(this ValidationEntryPoint<TEntity, TKey> entryPoint, string value, string propertyName)
			where TEntity : Entity<TKey>
		{
			if (value == string.Empty)
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName} is empty");
		}

		public static void IsInRange<TEntity, TKey, TValue>(this ValidationEntryPoint<TEntity, TKey> entryPoint, TValue value, TValue min, TValue max, string propertyName)
			where TEntity : Entity<TKey>
			where TValue : IComparable<TValue>
		{
			if (value.CompareTo(min) < 0)
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName}'s value\"{value}\" is less than {min}");

			if (value.CompareTo(max) > 0)
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName}'s value\"{value}\" is over {max}");
		}

		public static void IsWhitespaces<TEntity, TKey>(this ValidationEntryPoint<TEntity, TKey> entryPoint, string value, string propertyName)
			where TEntity : Entity<TKey>
		{
			if (value.Trim() == string.Empty)
				entryPoint.Entity.ValidationFail(propertyName, $"{propertyName}'s value \"{value}\" consists of whitespaces only");
		}

		public static void IsLengthOver<TEntity, TKey>(this ValidationEntryPoint<TEntity, TKey> entryPoint, string value, int maxLength, string propertyName)
			where TEntity : Entity<TKey>
		{
			if (value.Length > maxLength)
				entryPoint.Entity.ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is over {maxLength}");
		}

		public static void IsLengthLess<TEntity, TKey>(this ValidationEntryPoint<TEntity, TKey> entryPoint, string value, int minLength, string propertyName)
			where TEntity : Entity<TKey>
		{
			if (value.Length < minLength)
				entryPoint.Entity.ValidationFail(propertyName, $"Length of {propertyName}'s value\"{value}\" is less than {minLength}");
		}

		public static void IsLengthInRage<TEntity, TKey>(this ValidationEntryPoint<TEntity, TKey> entryPoint, string value, int minLength, int maxLength, string propertyName)
			where TEntity : Entity<TKey>
		{
			IsLengthLess(entryPoint, value, minLength, propertyName);
			IsLengthOver(entryPoint, value, maxLength, propertyName);
		}
	}
}