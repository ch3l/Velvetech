using System;
using System.Collections.Generic;

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

		public static User Build(string id)
		{
			return new User()
			{
				Id = id
			};
		}
	}
}
