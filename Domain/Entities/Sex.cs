using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities
{
	public class Sex : Entity<int>, IAggregateRoot
	{
		private List<Student> _student = new List<Student>();

		public Sex(int id, string name)
			: base(id)
		{
			Name = name;
		}

		public virtual IReadOnlyList<Student> Student => _student.AsReadOnly();

		public string Name { get; private set; }
	}
}
