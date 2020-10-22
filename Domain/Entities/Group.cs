using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities
{
	public class Group : Entity<Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		private readonly HashSet<Grouping> _grouping = new HashSet<Grouping>();
		public IReadOnlyCollection<Grouping> Grouping => _grouping;

		public Group(Guid id, string name)
			: base(id)
		{
			Name = name;
		}

		public void IncludeStudent(Student student)
		{
			var groupingEntry = new Grouping(student, this);
			//if (_grouping.Contains(groupingEntry))
			//	return;
			
			_grouping.Add(groupingEntry);
		}

		public void ExcludeStudent(Student student)
		{
			var groupingEntry = new Grouping(student, this);
			//if (_grouping.Contains(groupingEntry))
			//	return;

			_grouping.Remove(groupingEntry);
		}
	}
}
