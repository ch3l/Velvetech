using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class UserSpecification : Specification<User>
	{
		public UserSpecification()
		{
			Query.Include(s => s.UserRole)
				.ThenInclude(g => g.Role);
		}
	}
}