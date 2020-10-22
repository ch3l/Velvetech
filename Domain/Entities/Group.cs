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

		private readonly List<Grouping> _grouping = new List<Grouping>();
		public IReadOnlyList<Grouping> Grouping => _grouping.AsReadOnly();

		public Group(Guid id, string name)
			: base(id)
		{
			Name = name;
		}

		public void AddStudent(Student student)
		{
			_grouping.Add(new Grouping(student, this));
		}
	}
}
