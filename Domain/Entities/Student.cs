using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validators;

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

		private Student()
		{
		}

		public static async Task<Student> BuildAsync(StudentValidator validator,
			int sexId,
			string firstname,
			string middlename,
			string lastname,
			string callsign)
		{
			var instance = new Student();

			instance.SelectValidator(validator);
			instance.SetSexId(sexId);
			instance.SetFirstname(firstname);
			instance.SetMiddlename(middlename);
			instance.SetLastname(lastname);
			await instance.SetCallsignAsync(callsign);

			return instance;
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
			Validate.Firstname(ref firstName);

			if (HasErrorsInProperty(nameof(Firstname)))
				return;

			Firstname = firstName;
		}

		public void SetMiddlename(string middlename)
		{
			Validate.Middlename(ref middlename);

			if (HasErrorsInProperty(nameof(Middlename)))
				return;

			Middlename = middlename;
		}

		public void SetLastname(string lastname)
		{
			Validate.Lastname(ref lastname);

			if (HasErrorsInProperty(nameof(Lastname)))
				return;

			Lastname = lastname;
		}

		public async Task SetCallsignAsync(string callsign)
		{
			Validate.Callsign(ref callsign);

			if (Callsign == callsign)
				return;

			await Validate.CallsignUniqueness(callsign);

			if (HasErrorsInProperty(nameof(Callsign)))
				return;

			Callsign = callsign;
		}

		public void SetSexId(int sexId)
		{
			Validate.SexId(sexId);
			if (HasErrorsInProperty(nameof(SexId)))

				return;

			SexId = sexId;
		}
	}
}
