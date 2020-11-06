using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	public class UniquenessValidationError: ValidationError
	{
		public UniquenessValidationError(string propertyName) 
			: base(propertyName)
		{
		}

		public override string ToString()
		{
			return $"{PropertyName} already has unique value";
		}
	}
}
