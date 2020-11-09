using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Velvetech.Presentation.Shared.Dtos;

namespace Velvetech.Web.Services.Results
{
	public class StudentActionResult
	{

	}

	public class SuccessfulStudentAction : StudentActionResult
	{
		public StudentDto Student { get; }

		public SuccessfulStudentAction(StudentDto student)
		{
			Student = student;
		}
	}
	public class StudentErrors : StudentActionResult
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
			Callsign = errorStrings.TryGetValue(nameof(CallSite), out var callsignErrors) ? callsignErrors : null;
		}
	}

	public class StudentNotFound : StudentActionResult
	{

	}
}