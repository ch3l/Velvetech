﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class IncludedStudentsSpecification : Specification<Student>
	{
		public IncludedStudentsSpecification(Guid groupId)
		{
			Query.Where(s => s.Grouping.Any(g => g.Group.Id == groupId));
		}
	}
}
