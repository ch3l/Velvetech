using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Entities.Validators;

namespace Velvetech.Domain.Entities
{
	public class Role : Entity<string>, IAggregateRoot
	{
		private readonly HashSet<UserRole> _userRole = new HashSet<UserRole>();
		public IReadOnlyCollection<UserRole> UserRole => _userRole;

		private Role()
		{
		}

		public Role(string id)
		{
			Id = id;
		}
	}
}
