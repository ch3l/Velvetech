using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validators;

namespace Velvetech.Domain.Entities
{
	public class User : Entity<string>, IAggregateRoot
	{
		private readonly HashSet<UserRole> _userRole = new HashSet<UserRole>();
		public IReadOnlyCollection<UserRole> UserRole => _userRole;

		private User()
		{
		}

		public User(string id)
		{
			Id = id;
		}

		public bool AddRole(Role role) => 
			_userRole.Add(new UserRole(this, role));

		public bool HasRole(Role role) =>
			_userRole.Contains(new UserRole(this, role));

		public Role[] GetRoles() =>
			_userRole.Select(ur => ur.Role).ToArray();
	}
}
