using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Shared.Requests
{
	public class AddStudentRequest
	{
		public AddStudentRequest(int sexId, string firstName, string middleName, string lastName, string callsign)
		{
			SexId = sexId;
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Callsign = callsign;
		}

		public int SexId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Callsign { get; set; }
	}
}
