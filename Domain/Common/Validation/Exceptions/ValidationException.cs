﻿using System;

using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Common.Validation.Exceptions
{
	public class MissedErrorsValidationProcessingException : Exception
	{
		public MissedErrorsValidationProcessingException(IValidatableEntity entity)
			: base($"Entity \"{entity.GetType().FullName}\" validation errors has not been processed")
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
