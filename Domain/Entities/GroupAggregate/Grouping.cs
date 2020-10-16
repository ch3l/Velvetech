using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Domain.Entities.GroupAggregate
{
	public class Grouping 
	{
		public Grouping(Guid studentId, Guid groupId)
		{
			StudentId = studentId;
			GroupId = groupId;
		}

		public Grouping(Student student, Group group)
			: this(student.Id, group.Id)
		{				
		}

		public Guid StudentId { get; private set; }
		public Guid GroupId { get; private set; }

		public Student Student { get; private set; }
		public Group Group { get; private set; }
	}
}
