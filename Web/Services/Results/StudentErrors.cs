using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Velvetech.Web.Services.Results
{
	public class StudentErrors : EntityActionResult
	{
		public string[] SexId { get; }
		public string[] Firstname { get; }
		public string[] Middlename { get; }
		public string[] Lastname { get; }
		public string[] Callsign { get; }

		public StudentErrors(IReadOnlyDictionary<string, string[]> errorStrings)
		{
			SexId = errorStrings.TryGetValue(nameof(SexId), out var sexIdErrors) ? sexIdErrors : null;
			Firstname = errorStrings.TryGetValue(nameof(Firstname), out var firstnameErrors) ? firstnameErrors : null;
			Middlename = errorStrings.TryGetValue(nameof(Middlename), out var middlenameErrors) ? middlenameErrors : null;
			Lastname = errorStrings.TryGetValue(nameof(Lastname), out var lastnameErrors) ? lastnameErrors : null;
			Callsign = errorStrings.TryGetValue(nameof(Callsign), out var callsignErrors) ? callsignErrors : null;
		}
	}
}