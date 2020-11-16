using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class StudentSpecification : Specification<Student>
	{
		public StudentSpecification()
		{
			Query.Include(s => s.Sex);
			Query.Include(s => s.Grouping)
				.ThenInclude(g => g.Group);
		}

		public StudentSpecification(int skip, int take)
			:this()
		{
			Query.Skip(skip).Take(take);
		}

		public StudentSpecification(string sex, string fullname, string callsign, string group)
			: this()
		{
			FilterStudents(sex, fullname, callsign, group);
		}

		public StudentSpecification(int skip, int take, string sex, string fullname, string callsign, string group)
			: this(skip, take)
		{
			FilterStudents(sex, fullname, callsign, group);
		}

		private void FilterStudents(string sex, string fullname, string callsign, string group)
		{
			//sex = FilterInputString(sex);
			//fullname = FilterInputString(fullname);
			//callsign = FilterInputString(callsign);
			//group = FilterInputString(group);			

			Query.Where(student =>
				(sex == null || sex == string.Empty || student.Sex.Name.Equals(sex))
				&& (fullname == null || (student.Firstname + " " + (student.Middlename ?? string.Empty) + " " + student.Lastname).Contains(fullname))
				&& (callsign == null || student.Callsign.Contains(callsign))
				&& (group == null || student.Grouping.Any(g => g.Group.Name.Contains(group))));
		}
	}
}
