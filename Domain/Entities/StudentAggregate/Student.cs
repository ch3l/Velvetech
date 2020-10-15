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
		/*
		private readonly List<Group> _group = new List<Group>();
		public IReadOnlyList<Group> Group => _group.AsReadOnly();  
		*/

		public int SexId { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string Callsign { get; private set; }

		public virtual Sex Sex { get; private set; }
		public virtual List<Grouping> Grouping { get; set; } = new List<Grouping>();

		public string Fullname() => FirstName + " " + MiddleName + " " + LastName;
	}
}
