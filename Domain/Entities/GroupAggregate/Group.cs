using System;
using System.Collections.Generic;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Velvetech.Domain.Entities.GroupAggregate
{
	public class Group : Entity<Guid>, IAggregateRoot
	{
		/*
		private readonly List<Student> _student = new List<Student>();
		public IEnumerable<Student> Student => _student.AsReadOnly(); 
		*/

		public string Name { get; private set; }

		public List<Grouping> Grouping { get; set; } = new List<Grouping>();
		//public virtual List<Student> Student { get; set; } = new List<Student>();

		/*
		public void AddStudent(Student student)
		{
			_student.Add(student);
		}*/
	}
}
