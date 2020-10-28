using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class IncludedStudentsSpecification : StudentSpecification
	{
		public IncludedStudentsSpecification()
		{
			Query.Include(s => s.Sex);
			Query.Include(s => s.Grouping)
				.ThenInclude(g => g.Group);
		}

		public IncludedStudentsSpecification(int skip, int take)
			:this()
		{
			Query.Skip(skip).Take(take);
		}

		public IncludedStudentsSpecification(string sex, string fullname, string callsign, string group)
			: this()
		{
			FilterStudents(sex, fullname, callsign, group);
		}

		public IncludedStudentsSpecification(int skip, int take, string sex, string fullname, string callsign, string group)
			: this(skip, take)
		{
			FilterStudents(sex, fullname, callsign, group);
		}

		private void FilterStudents(string sex, string fullname, string callsign, string group)
		{
			Query.Where(student =>
				(sex == null || student.Sex.Name.Equals(sex))
				&& (fullname == null || (student.Firstname + " " + student.Middlename + " " + student.Lastname).Contains(fullname))
				&& (callsign == null || student.Callsign.Contains(callsign))
				&& (group == null || student.Grouping.Any(g => g.Group.Name.Contains(group))));
		}
	}
}
