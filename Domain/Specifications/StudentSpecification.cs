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
			//Query.Where(s => s.Grouping.Count > 0);
		}

		public StudentSpecification(int skip, int take)
			:this()
		{
			Query.Skip(skip).Take(take);
		}

		public StudentSpecification(string sex, string fullname, string callsign, string group)
			: this()
		{
			Query.Where(student =>
				(sex == null || student.Sex.Name.Equals(sex))
				&& (fullname == null || (student.Firstname + " " + student.Middlename + " " + student.Lastname).Contains(fullname))
				&& (callsign == null || student.Callsign.Contains(callsign))
				&& (group == null || student.Grouping.Any(g => g.Group.Name.Contains(group))));
		}

		public StudentSpecification(int skip, int take, string sex, string fullname, string callsign, string group)
			: this(skip, take)
		{
			Query.Where(student =>
				(sex == null || student.Sex.Name.Equals(sex))							   
				&& (fullname == null || (student.Firstname + " " + student.Middlename + " " + student.Lastname).Contains(fullname))
				&& (callsign == null || student.Callsign.Contains(callsign))
				&& (group == null || student.Grouping.Any(g => g.Group.Name.Contains(group))));
		}
	}
}
