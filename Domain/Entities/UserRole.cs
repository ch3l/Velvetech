using System;
using Velvetech.Domain.Common;

namespace Velvetech.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string UserId { get; }
        public string RoleId { get; }

        public Role Role { get; }
        public User User { get; }

        public UserRole(string userId, string roleId)
        {
	        UserId = userId;
	        RoleId = roleId;
        }

        public UserRole(User user, Role role)
	        : this(user.Id, role.Id)
        {
	        User = user;
	        Role = role;
        }

		public override bool Equals(object obj) =>
	        obj is UserRole grouping
	        && UserId == grouping.UserId
	        && RoleId == grouping.RoleId;

        public override int GetHashCode() =>
	        HashCode.Combine(UserId, RoleId);
	}
}
