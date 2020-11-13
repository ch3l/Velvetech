using System;
using System.Collections.Generic;
using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validators;

namespace Velvetech.Domain.Entities
{
	public class Role : ValidatableEntity<Guid, GroupValidator>, IAggregateRoot
	{
		public string Name { get; private set; }

		private readonly HashSet<UserRole> _userRole = new HashSet<UserRole>();
		public IReadOnlyCollection<UserRole> UserRole => _userRole;

		private Role()
		{
		}

		public static Role Build(GroupValidator validator, string name)
		{
			var instance = new Role();

			instance.SelectValidator(validator);
			instance.SetName(name);

			return instance;
		}

		public void SetName(string name)
		{
			Validate.Name(ref name);

			if (HasErrorsInProperty(nameof(Name)))
				return;

			Name = name;
		}
	}
}
