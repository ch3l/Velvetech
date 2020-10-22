using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Velvetech.Domain.Common;
using System.Data;

namespace Velvetech.Domain.Entities
{
	public class Grouping : BaseEntity, IAggregateRoot
	{
		public Grouping(Guid studentId, Guid groupId)
		{
			StudentId = studentId;
			GroupId = groupId;
		}

		public Grouping(Student student, Group group)
			: this(student.Id, group.Id)
		{
			Student = student;
			Group = group;
		}

		public Guid StudentId { get; private set; }
		public Guid GroupId { get; private set; }

		public Student Student { get; private set; }
		public Group Group { get; private set; }
	}
}
