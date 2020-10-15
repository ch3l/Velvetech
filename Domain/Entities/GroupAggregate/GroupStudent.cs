using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Domain.Entities.GroupAggregate
{
	public class GroupStudent : Entity<Guid>, IAggregateRoot
	{
		public Guid StudentId { get; set; }
		public Guid GroupId { get; set; }

		public Student Student { get; set; }
		public Group Group { get; set; }
	}
}
