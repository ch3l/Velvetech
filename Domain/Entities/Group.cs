using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;

namespace Velvetech.Domain.Entities
{
	public class Group : ValidatableEntity<Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		private readonly HashSet<Grouping> _grouping = new HashSet<Grouping>();
		public IReadOnlyCollection<Grouping> Grouping => _grouping;

		public void SetName(string name)
		{
			ValidateName(name);
			if (HasValidationErrors)
				return;

			Name = name;
		}

		public bool IncludeStudent(Guid studentId)
		{
			var groupingEntry = new Grouping(studentId, Id);
			return _grouping.Add(groupingEntry);
		}

		public bool ExcludeStudent(Guid studentId)
		{
			var groupingEntry = new Grouping(studentId, Id);
			return _grouping.Remove(groupingEntry);
		}

		public bool ExcludeAllStudents()
		{
			if (_grouping.Count > 0)
			{
				_grouping.Clear();
				return true;
			}

			return false;
		}

		private void ValidateName(string name)
		{
			if (name is null)
			{
				ValidationFail(nameof(Name), $"{nameof(Name)} is null");
				return;
			}

			if (name == string.Empty)
				ValidationFail(nameof(Name), $"{nameof(Name)} is empty");

			if (name.Trim() == string.Empty)
				ValidationFail(nameof(Name), $"Value \"{name}\" of property {nameof(Name)} consists of whitespaces only");

			if (name.Length > 25)
				ValidationFail(nameof(Name), $"Length {nameof(Name)} value \"{name}\" is over 25");
		}
	}
}
