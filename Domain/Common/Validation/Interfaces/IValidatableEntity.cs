using System.Collections.Generic;

namespace Velvetech.Domain.Common.Validation.Interfaces
{
	public interface IValidatableEntity
	{
		bool HasValidationErrors { get; }
		bool HasValidator { get; }
		public IDictionary<string, string[]> Errors { get; }
	}
}