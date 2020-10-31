using System;
using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validations
{
	public static class StudentValidationExtensions
	{
		public static void Firstname(this ValidationEntryPoint<Student, Guid> validation, string value, string propertyName)
		{
			if (validation.IsNull(value, propertyName))
				return;

			validation.IsEmpty(value, propertyName);
			validation.IsWhitespaces(value, propertyName);
			validation.IsLengthOver(value, 40, propertyName);
		}

		public static void Middlename(this ValidationEntryPoint<Student, Guid> validation, string value, string propertyName)
		{
			if (value is null)
				return;

			validation.IsEmpty(value, propertyName);
			validation.IsWhitespaces(value, propertyName);
			validation.IsLengthOver(value, 60, propertyName);
		}

		public static void Lastname(this ValidationEntryPoint<Student, Guid> validation, string value, string propertyName)
		{
			if (validation.IsNull(value, propertyName))
				return;

			validation.IsEmpty(value, propertyName);
			validation.IsWhitespaces(value, propertyName);
			validation.IsLengthOver(value, 40, propertyName);
		}

		public static void Callsign(this ValidationEntryPoint<Student, Guid> validation, string value, string propertyName)
		{
			if (value is null)
				return;

			validation.IsEmpty(value, propertyName);
			validation.IsWhitespaces(value, propertyName);
			validation.IsLengthInRage(value, 6, 16, propertyName);
		}

		public static void SexId(this ValidationEntryPoint<Student, Guid> validation, int sexId, string propertyName)
		{
			validation.IsInRange(sexId, 1, 2, propertyName);
		}
	}

	public static class GroupValidationExtensions
	{
		public static void Name(this ValidationEntryPoint<Group, Guid> validation, string value, string propertyName)
		{
			if (validation.IsNull(value, propertyName))
				return;

			validation.IsEmpty(value, propertyName);
			validation.IsWhitespaces(value, propertyName);
			validation.IsLengthOver(value, 25, propertyName);
		}
	}
}
