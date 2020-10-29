using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
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
			  		  

		public Student(int sexId, string firstname, string middlename, string lastname, string callsign)
		{
			SetSexId(sexId);
			SetFirstname(firstname);
			SetMiddlename(middlename);
			SetLastname(lastname);
			SetCallsign(callsign);
		}

		public bool ExcludeFromAllGroups()
		{
			if (_grouping.Count > 0)
			{
				_grouping.Clear();
				return true;
			}

			return false;
		}

		public void SetFirstname(string firstName)
		{
			Firstname = firstName;
		}

		public void SetMiddlename(string middlename)
		{
			Middlename = middlename;
		}

		public void SetLastname(string lastname)
		{
			Lastname = lastname;
		}

		public void SetCallsign(string callsign)
		{
			Callsign = callsign;
		}

		public void SetSexId(int sexId)
		{
			SexId = sexId;
		}
	}
}
