using System.Reflection.Emit;

namespace Presentation.Shared.Dtos
{
	public class StudentDto
	{
		public StudentDto()
		{

		}

		public StudentDto(
			string firstName,
			string middleName,
			string lastName,
			string callsign,
			SexDto sex,
			GroupDto[] groups
			)
		{
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Callsign = callsign;
			Sex = sex;
			Groups = groups;
		}

		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Callsign { get; set; }

		public SexDto Sex { get; set; }
		public GroupDto[] Groups { get; set; }
	}
}
