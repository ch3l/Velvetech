using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentGroupAggregate;

namespace Velvetech.Domain.Entities.GroupAggregate
{
	public class Group : Entity<Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		private List<Grouping> _grouping = new List<Grouping>();
		public virtual IReadOnlyList<Grouping> Grouping => _grouping.AsReadOnly();

		public Group(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
