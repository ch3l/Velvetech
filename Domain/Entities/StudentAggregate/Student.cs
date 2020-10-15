using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.GroupAggregate;	  

namespace Velvetech.Domain.Entities.StudentAggregate
{
	public class Student : Entity<Guid>, IAggregateRoot
	{
		private List<Group> _groups = new List<Group>();
		public IReadOnlyList<Group> Groups => _groups.AsReadOnly();

		public Sex Sex { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string Callsign { get; private set; }

		public List<GroupStudent> Grouping { get; set; } = new List<GroupStudent>();

		public string FullName => FirstName + " " + MiddleName + " " + LastName;
	}
}
