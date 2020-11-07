using System;
using System.Collections.Generic;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validations;

namespace Velvetech.Domain.Entities
{
	public class Group : ValidatableEntity<Guid, GroupValidator>, IAggregateRoot
	{
		public string Name { get; private set; }

		private readonly HashSet<Grouping> _grouping = new HashSet<Grouping>();
		public IReadOnlyCollection<Grouping> Grouping => _grouping;

		private Group()
		{
		}

		public static Group Build(GroupValidator validator, string name)
		{
			var instance = new Group();

			instance.SelectValidator(validator);
			instance.SetName(name);

			return instance;
		}

		public void SetName(string name)
		{
			Validate.Name(ref name);

			if (HasValidationErrors)
				return;

			Name = name;
		}

		public bool IncludeStudent(Student student)
		{
			var groupingEntry = new Grouping(student, this);
			return _grouping.Add(groupingEntry);
		}

		public bool ExcludeStudent(Student student)
		{
			var groupingEntry = new Grouping(student.Id, Id);
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
