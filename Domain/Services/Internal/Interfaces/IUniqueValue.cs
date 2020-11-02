using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;

namespace Velvetech.Domain.Services.Internal.Interfaces
{
	interface IValidationService<TValue>
	{
		public bool IsValueAlreadyExists(TValue value);
	}
}
