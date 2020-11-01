using System;
using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities.Validations
{
	public class StudentValidator : Validator
	{
		public void Firstname(string value, string propertyName)
		{
			if (IsNull(value, propertyName))
				return;

			if (IsEmpty(value, propertyName))
				return;
			
			IsWhitespaces(value, propertyName);
			IsExceedSize(value, 40, propertyName);
		}

		public void Middlename(ref string value, string propertyName)
		{
			if (value is null)
				return;

			if (EmptyAsNull(ref value, propertyName))
				return;

			IsWhitespaces(value, propertyName);
			IsExceedSize(value, 60, propertyName);
		}

		public void Lastname(string value, string propertyName)
		{
			if (IsNull(value, propertyName))
				return;

			if (IsEmpty(value, propertyName))
				return;
			
			IsWhitespaces(value, propertyName);
			IsExceedSize(value, 40, propertyName);
		}

		public void Callsign(ref string value, string propertyName)
		{
			if (value is null)
				return;

			if (EmptyAsNull(ref value, propertyName))
				return;
			
			IsWhitespaces(value, propertyName);
			IsCountOutOfRange(value, 6, 16, propertyName);
		}

		public void SexId(int sexId, string propertyName)
		{
			IsOutOfRange(sexId, 1, 2, propertyName);
		}
	}
}
