using Ardalis.Specification;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Specifications
{
	public class GroupSpecification : Specification<Group>
	{
		public GroupSpecification()
		{
			Query.Include(s => s.Grouping)
				.ThenInclude(g => g.Student);
		}

		public GroupSpecification(string groupName)
			:this()
		{
			Query.Where(group => groupName == null || group.Name.Contains(groupName));
		}
	}
}