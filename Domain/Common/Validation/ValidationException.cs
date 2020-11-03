using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation
{
	public class MissedValidationErrorsProcessingException : Exception
	{
		public MissedValidationErrorsProcessingException(IValidatableEntity entity)
			:base($"Entity \"{entity.GetType().FullName}\" validation errors has not been processed")
		{
		}
	}
}
