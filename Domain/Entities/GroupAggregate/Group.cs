using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Domain.Entities.GroupAggregate
{
	public class Group : Entity<Guid>, IAggregateRoot
	{
		private List<Grouping> _grouping = new List<Grouping>();
		public IReadOnlyList<Grouping> Grouping => _grouping.AsReadOnly();

		public string Name { get; private set; }

		public void AddStudent(Student student)
		{
			if (Grouping.FirstOrDefault(gr => gr.StudentId == student.Id) != null)
				return;

			_grouping.Add(new Grouping(student, this));
		}
	}
}
