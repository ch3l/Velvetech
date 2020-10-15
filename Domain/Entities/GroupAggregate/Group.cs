using System;
using System.Collections.Generic;
using System.Text;

using Domain.Common;
using Domain.Entities.StudentAggregate;

namespace Domain.Entities.GroupAggregate
{
	class Group : Entity<Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		List<Student> _students = new List<Student>();

		public IEnumerable<Student> Students => _students;

		public void AddStudent(Student student)
		{
			_students.Add(student);
		}
	}
}
