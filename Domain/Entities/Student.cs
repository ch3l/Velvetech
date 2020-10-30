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

		private void ValidateCallsign(string callsign)
		{
			if (callsign is null)
				return;

			if (callsign == string.Empty)
				ValidationFail(nameof(Callsign), $"{nameof(Callsign)} is empty");

			if (callsign.Trim() == string.Empty)
				ValidationFail(nameof(Callsign), $"Value \"{callsign}\" of property {nameof(Callsign)} consists of whitespaces only");

			if (callsign.Length < 6)
				ValidationFail(nameof(Callsign), $"Length of {nameof(Callsign)}'s value\"{callsign}\" is less than 6");

			if (callsign.Length > 16)
				ValidationFail(nameof(Callsign), $"Length of {nameof(Callsign)}'s value\"{callsign}\" is over 16");
		}

		private void ValidateFirstname(string firstname)
		{
			if (firstname is null)
			{
				ValidationFail(nameof(Firstname), $"{nameof(Firstname)} is null");
				return;
			}

			if (firstname == string.Empty)
				ValidationFail(nameof(Firstname), $"{nameof(Firstname)} is empty");

			if (firstname.Trim() == string.Empty)
				ValidationFail(nameof(Firstname), $"Value \"{firstname}\" of property {nameof(Firstname)} consists of whitespaces only");

			if (firstname.Length > 40)
				ValidationFail(nameof(Firstname), $"Length of {nameof(Firstname)}'s value\"{firstname}\" is over 40");
		}

		private void ValidateLastname(string lastname)
		{
			if (lastname is null)
			{
				ValidationFail(nameof(Lastname), $"{nameof(Lastname)} is null");
				return;
			}

			if (lastname == string.Empty)
				ValidationFail(nameof(Lastname), $"{nameof(Lastname)} is empty");

			if (lastname.Trim() == string.Empty)
				ValidationFail(nameof(Lastname), $"Value \"{lastname}\" of property {nameof(Lastname)} consists of whitespaces only");

			if (lastname.Length > 40)
				ValidationFail(nameof(Lastname), $"Length of {nameof(Lastname)}'s value\"{lastname}\" is over 40");
		}

		private void ValidateMiddlename(string middlename)
		{
			if (middlename is null)
				return;

			if (middlename == string.Empty)
				ValidationFail(nameof(Middlename), $"{nameof(Middlename)} is empty");

			if (middlename.Trim() == string.Empty)
				ValidationFail(nameof(Middlename), $"Value \"{middlename}\" of property {nameof(Middlename)} consists of whitespaces only");

			if (middlename.Length > 60)
				ValidationFail(nameof(Middlename), $"Length of {nameof(Middlename)}'s value\"{middlename}\" is over 60");
		}
	}
}
