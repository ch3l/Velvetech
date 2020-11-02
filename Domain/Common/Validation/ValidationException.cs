using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public class ValidatorMissingException : Exception
	{
		public ValidatorMissingException(IValidatableEntity entity)
			:base($"Entity \"{entity.GetType().FullName}\" has no selected validator")
		{
		}
	}
}
