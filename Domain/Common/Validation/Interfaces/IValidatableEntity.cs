using System.Collections.Generic;
using Velvetech.Domain.Common.Validation.Errors.Base;

namespace Velvetech.Domain.Common.Validation.Interfaces
{
	public interface IValidatableEntity
	{
		bool HasErrors { get; }
		bool HasValidator { get; }
		public IReadOnlyDictionary<string, string[]> ErrorsStrings { get; }
		public IReadOnlyDictionary<string, ValidationError[]> Errors { get; }
	}
}