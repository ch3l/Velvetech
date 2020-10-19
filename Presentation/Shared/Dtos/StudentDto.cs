using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;

using Presentation.Shared.Requests;

namespace Presentation.Shared.Dtos
{
	public class StudentDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Callsign { get; set; }

		public SexDto Sex { get; set; }
		public IEnumerable<GroupDto> Groups { get; set; }

		public string FullName => FirstName + " " + MiddleName + " " + LastName;
	}
}
