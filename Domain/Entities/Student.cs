using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

using LinqKit;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validations;

namespace Velvetech.Domain.Entities
{
	public class Student : ValidatableEntity<Guid, StudentValidator>, IAggregateRoot
	{
		public string Firstname { get; private set; }
		public string Middlename { get; private set; }
		public string Lastname { get; private set; }
		public string Callsign { get; private set; }

		public int SexId { get; private set; }
		public Sex Sex { get; private set; }

		private readonly List<Grouping> _grouping = new List<Grouping>();
		public IReadOnlyList<Grouping> Grouping => _grouping.AsReadOnly();
	
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
			Validator.Firstname(firstName, nameof(Firstname));
			if (HasValidationErrors)
				return;
			
			Firstname = firstName;
		}

		public void SetMiddlename(string middlename)
		{
			Validator.Middlename(middlename, nameof(Middlename));
			if (HasValidationErrors)
				return;

			Middlename = middlename;
		}

		public void SetLastname(string lastname)
		{
			Validator.Lastname(lastname, nameof(Lastname));
			if (HasValidationErrors)
				return;

			Lastname = lastname;
		}

		public void SetCallsign(string callsign)
		{
			Validator.Callsign(callsign, nameof(Callsign));
			if (HasValidationErrors)
				return;

			Callsign = callsign;
		}

		public void SetSexId(int sexId)
		{
			Validator.SexId(sexId, nameof(SexId));
			if (HasValidationErrors)
				return;

			SexId = sexId;
		}
	}
}
