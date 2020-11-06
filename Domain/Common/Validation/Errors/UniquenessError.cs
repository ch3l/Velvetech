using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	public class UniquenessValidationError<TValue> : DetailedValidationError<TValue>
	{
		public UniquenessValidationError(string propertyName, TValue value)  
			: base(propertyName, value)
		{
		}

		public override string ToString()
		{
			return $"{PropertyName} already has unique value \"{Value}\"";
		}
	}
}
