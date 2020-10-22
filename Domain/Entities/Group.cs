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

		public bool IncludeStudent(Guid studentId)
		{
			var groupingEntry = new Grouping(studentId, Id);
			//if (_grouping.Contains(groupingEntry))
			//	return;
			
			return _grouping.Add(groupingEntry);
		}

		public bool ExcludeStudent(Guid studentId)
		{
			var groupingEntry = new Grouping(studentId, Id);
			//if (_grouping.Contains(groupingEntry))
			//	return;

			return _grouping.Remove(groupingEntry);
		}

		public bool ExcludeAllStudents()
		{
			if (_grouping.Count > 0)
			{
				_grouping.Clear();
				return true;
			}

			return false;
		}
	}
}
