using System;
using System.Collections.Generic;
using System.Linq;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentGroupAggregate;

namespace Velvetech.Domain.Entities.StudentAggregate
{
	public class Student : Entity<Guid>, IAggregateRoot
	{
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string Callsign { get; private set; }

		public int SexId { get; private set; }
		public Sex Sex { get; private set; }

		private List<Grouping> _grouping = new List<Grouping>();
		public IReadOnlyList<Grouping> Grouping => _grouping.AsReadOnly();

		public Student(int sexId, string firstName, string middleName, string lastName, string callsign)
		{
			SexId = sexId;
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Callsign = callsign;
		}
	}
}
