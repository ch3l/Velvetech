using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Domain.Entities.GroupAggregate
{
	public class Group : Entity<Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		List<Student> _students = new List<Student>();

		public IEnumerable<Student> Students => _students;

		public List<GroupStudent> Grouping { get; set; } = new List<GroupStudent>();

		public void AddStudent(Student student)
		{
			_students.Add(student);
		}
	}
}
