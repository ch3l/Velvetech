using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public class ValidationException : Exception
	{
		public ValidationException(IValidatableEntity entity)
		{
			var stringBuilder = new StringBuilder();

			foreach (var (propertyName, errorsList) in entity.Errors)
			{
				stringBuilder.Append($"Property \"{propertyName}\" has following validation errors:\n");
				foreach (var errorText in errorsList)
					stringBuilder.Append(errorText);
			}
		}
	}
}
