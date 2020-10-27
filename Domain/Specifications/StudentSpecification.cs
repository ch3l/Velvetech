using System;
using System.Collections.Generic;
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
			Query.Where(s => s.Grouping.Count > 0);
		}
	}
	
	public class GroupSpecification : Specification<Group>
	{
		public GroupSpecification()
		{
			Query.Include(s => s.Grouping)
				.ThenInclude(g => g.Student);
		}
	}
}
