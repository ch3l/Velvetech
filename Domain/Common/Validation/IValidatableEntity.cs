using System.Collections.Generic;

namespace Velvetech.Domain.Common.Validation
{
	public interface IValidatableEntity
	{
		bool HasValidationErrors { get; }
		public IDictionary<string, string[]> Errors { get; }
	}
}