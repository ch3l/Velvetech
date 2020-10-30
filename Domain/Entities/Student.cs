using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

using LinqKit;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities
{
	public class Student : ValidatableEntity<Guid>, IAggregateRoot
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
			ValidateCallsign(callsign);
			if (HasValidationErrors)
				return;

			Callsign = callsign;
		}

		public void SetSexId(int sexId)
		{
			ValidateSexId(sexId);
			if (HasValidationErrors)
				return;

			SexId = sexId;
		}

		private void ValidateSexId(int sexId)
		{
			if (sexId < 1 || sexId > 2)
				ValidationFail(nameof(SexId), $"{sexId} is incorrect SexId value");
		}

		private void ValidateCallsign(string callsign)
		{
			if (callsign is null)
				return;

			if (callsign.Length < 6)
				ValidationFail(nameof(Callsign), $"Length of Callsign \"{callsign}\" is less than 6");

			if (callsign.Length > 16)
				ValidationFail(nameof(Callsign), $"Length of Callsign \"{callsign}\" is over 16");
		}
	}
}
