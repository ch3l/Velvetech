using System;
using System.Collections.Generic;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validations;

namespace Velvetech.Domain.Entities
{
	public class Group : ValidatableEntity<Group, Guid>, IAggregateRoot
	{
		public string Name { get; private set; }

		private readonly HashSet<Grouping> _grouping = new HashSet<Grouping>();
		public IReadOnlyCollection<Grouping> Grouping => _grouping;

		public void SetName(string name)
		{
			Validation.Name(name, nameof(Name));
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
	}
}
