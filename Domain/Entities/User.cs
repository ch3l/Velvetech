using System;
using System.Collections.Generic;
using System.Linq;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validators;

namespace Velvetech.Domain.Entities
{
	public class User : ValidatableEntity<Guid, UserValidator>, IAggregateRoot
	{
		public string Name { get; private set; }

		private readonly HashSet<UserRole> _userRole = new HashSet<UserRole>();
		public IReadOnlyCollection<UserRole> UserRole => _userRole;

		private User()
		{
		}

		public static User Build(UserValidator validator, string name)
		{
			var instance = new User();

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
					 

		public bool AddRole(Role role) => 
			_userRole.Add(new UserRole(this, role));

		public Role[] GetRoles() => 
			_userRole.Select(ur => ur.Role).ToArray();

		public bool RemoveRole(Role role) =>
			_userRole.Remove(new UserRole(this, role));

		public void RemoveAllRoles() =>
			_userRole.Clear();
	}
}
