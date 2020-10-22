using System;
using System.Collections.Generic;
using System.Linq;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities
{
	public class Student : Entity<Guid>, IAggregateRoot
	{
		public string Firstname { get; private set; }
		public string Middlename { get; private set; }
		public string Lastname { get; private set; }
		public string Callsign { get; private set; }

		public int SexId { get; private set; }
		public Sex Sex { get; private set; }

		private readonly List<Grouping> _grouping = new List<Grouping>();
		public IReadOnlyList<Grouping> Grouping => _grouping.AsReadOnly();

		public Student(Guid id, int sexId, string firstname, string middlename, string lastname, string callsign)
			: base(id)
		{
			SexId = sexId;
			Firstname = firstname;
			Middlename = middlename;
			Lastname = lastname;
			Callsign = callsign;
		}

		public Student(int sexId, string firstname, string middlename, string lastname, string callsign)
		{
			SexId = sexId;
			Firstname = firstname;
			Middlename = middlename;
			Lastname = lastname;
			Callsign = callsign;
		}
	}
}
