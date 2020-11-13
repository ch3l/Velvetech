using System;

namespace Velvetech.Domain.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }

        public UserRole(Guid userId, Guid roleId)
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
