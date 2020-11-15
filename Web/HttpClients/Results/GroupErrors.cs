using System.Collections.Generic;

namespace Velvetech.Web.HttpClients.Results
{
	public class GroupErrors : EntityActionResult
	{
		public string[] Name { get; }

		public GroupErrors(IReadOnlyDictionary<string, string[]> errorStrings)
		{
			Name = errorStrings.TryGetValue(nameof(Name), out var nameErrors) ? nameErrors: null;
		}
	}
}