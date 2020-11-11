using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities
{
	public class Sex : Entity<int>, IAggregateRoot
	{
		private List<Student> _student = new List<Student>();

		private Sex()
		{

		}

		public Sex(string name)
		{
			Name = name;
		}

		public IReadOnlyList<Student> Student => _student.AsReadOnly();

		public string Name { get; private set; }
	}
}
