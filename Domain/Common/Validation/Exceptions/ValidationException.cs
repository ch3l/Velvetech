using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation.Exceptions
{
	public class MissedValidationErrorsProcessingException : Exception
	{
		public MissedValidationErrorsProcessingException(IValidatableEntity entity)
			:base($"Entity \"{entity.GetType().FullName}\" validation errors has not been processed")
		{
		}
	}

	public class NotSelectedValidatorException : Exception 
	{
		public NotSelectedValidatorException(Type entityType)
			: base($"Validator has not been selected for {entityType.Name}")
		{
		}
	}
}
