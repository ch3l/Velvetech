using System.Collections.Generic;
using Velvetech.Web.HttpClients.Results.Base;

namespace Velvetech.Web.HttpClients.Results
{
	public class GroupErrors : ClientActionResult
	{
		public string[] Name { get; }

		public GroupErrors(IReadOnlyDictionary<string, string[]> errorStrings)
		{
			Name = errorStrings.TryGetValue(nameof(Name), out var nameErrors) ? nameErrors: null;
		}
	}
}