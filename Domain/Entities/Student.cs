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
			ValidateFirstname(firstName);
			if (HasValidationErrors)
				return;
			
			Firstname = firstName;
		}

		public void SetMiddlename(string middlename)
		{
			ValidateMiddlename(middlename);
			if (HasValidationErrors)
				return;
			

			Middlename = middlename;
		}

		public void SetLastname(string lastname)
		{
			ValidateLastname(lastname);
			if (HasValidationErrors)
				return;

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

		private void ValidateFirstname(string firstname)
		{
			if (Validation.IsNull(firstname, nameof(Firstname)))
				return;
				
			Validation.IsEmpty(firstname, nameof(Firstname));
			Validation.IsWhiteSpaces(firstname, nameof(Firstname));
			Validation.IsLengthOver(firstname, 40, nameof(Firstname));
		}

		private void ValidateMiddlename(string middlename)
		{
			if (middlename is null)
				return;

			Validation.IsEmpty(middlename, nameof(Middlename));
			Validation.IsWhiteSpaces(middlename, nameof(Middlename));
			Validation.IsLengthOver(middlename, 60, nameof(Middlename));
		}

		private void ValidateLastname(string lastname)
		{
			if (Validation.IsNull(lastname, nameof(Lastname)))
				return;
				
			Validation.IsEmpty(lastname, nameof(Lastname));
			Validation.IsWhiteSpaces(lastname, nameof(Lastname));
			Validation.IsLengthOver(lastname, 40, nameof(Lastname));
		}
		private void ValidateCallsign(string callsign)
		{
			if (callsign is null)
				return;

			Validation.IsEmpty(callsign, nameof(Callsign));
			Validation.IsWhiteSpaces(callsign, nameof(Callsign));
			Validation.IsLengthLess(callsign, 6, nameof(Callsign));
			Validation.IsLengthOver(callsign, 16, nameof(Callsign));
		}
	}
}
