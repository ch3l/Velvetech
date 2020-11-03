using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
			if (Firstname == firstName)
				return;

			Validate.Firstname(ref firstName);
			if (HasValidationErrors)
				return;

			Firstname = firstName;
		}

		public void SetMiddlename(string middlename)
		{
			if (Middlename == middlename)
				return;

			Validate.Middlename(ref middlename);
			if (HasValidationErrors)
				return;

			Middlename = middlename;
		}

		public void SetLastname(string lastname)
		{
			if (Lastname == lastname)
				return;

			Validate.Lastname(ref lastname);
			if (HasValidationErrors)
				return;

			Lastname = lastname;
		}

		public async Task SetCallsignAsync(string callsign)
		{
			if (Callsign == callsign)
				return;

			Validate.Callsign(ref callsign);
			await Validate.CallsignUniqueness(callsign);
			if (HasValidationErrors)
				return;

			Callsign = callsign;
		}

		public void SetSexId(int sexId)
		{
			if (SexId == sexId)
				return;

			Validate.SexId(sexId);
			if (HasValidationErrors)
				return;

			SexId = sexId;
		}
	}
}
