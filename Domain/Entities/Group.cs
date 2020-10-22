using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities
{
	public class Group : Entity<Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		public List<Grouping> Grouping { get; set; } = new List<Grouping>();

		public Group(Guid id, string name)
			: base(id)
		{
			Name = name;
		}
	}
}
