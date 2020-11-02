using System;
using System.Collections.Generic;
using System.Text;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Errors
{
	class UniquenessError: ValidationError
	{
		public UniquenessError(string propertyName) 
			: base(propertyName)
		{
		}

		public override string ToString()
		{
			return $"{PropertyName} already has unique value";
		}
	}
}
