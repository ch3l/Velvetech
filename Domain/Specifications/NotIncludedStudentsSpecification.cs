using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class NotIncludedStudentsSpecification : Specification<Student>
	{
		public NotIncludedStudentsSpecification(Guid groupId)
		{
			Query.Where(s => s.Grouping.All(g => g.GroupId != groupId));
		}
	}
}
