using System.Collections.Generic;
using System.Linq;

namespace Velvetech.Domain.Common.Validation
{
	public interface IValidatableEntity
	{
		bool HasValidationErrors { get; }
		public IDictionary<string, string[]> Errors { get; }
	}

	public abstract class ValidatableEntity<TKey> : Entity<TKey>, IValidatableEntity
	{
		private Dictionary<string, List<string>> _errors;

		public bool HasValidationErrors => _errors != null && _errors.Count > 0;

		protected void ValidationFail(string propertyName, string error)
		{
			_errors ??= new Dictionary<string, List<string>>();

			if (!_errors.TryGetValue(propertyName, out var errorsList))
			{
				errorsList = new List<string>();
				_errors[propertyName] = errorsList;
			}

			errorsList.Add(error);
		}

		public IDictionary<string, string[]> Errors =>
			_errors is null
				? new Dictionary<string, string[]>() 
				: _errors.ToDictionary(
					pair => pair.Key,
					pair => pair.Value.ToArray());
	}
}
