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

			IsEmpty(value, propertyName);
			IsWhitespaces(value, propertyName);
			IsLengthOver(value, 40, propertyName);
		}

		public void Middlename(string value, string propertyName)
		{
			if (value is null)
				return;

			IsEmpty(value, propertyName);
			IsWhitespaces(value, propertyName);
			IsLengthOver(value, 60, propertyName);
		}

		public void Lastname(string value, string propertyName)
		{
			if (IsNull(value, propertyName))
				return;

			IsEmpty(value, propertyName);
			IsWhitespaces(value, propertyName);
			IsLengthOver(value, 40, propertyName);
		}

		public void Callsign(string value, string propertyName)
		{
			if (value is null)
				return;

			IsEmpty(value, propertyName);
			IsWhitespaces(value, propertyName);
			IsLengthInRage(value, 6, 16, propertyName);
		}

		public void SexId(int sexId, string propertyName)
		{
			IsInRange(sexId, 1, 2, propertyName);
		}
	}
}
