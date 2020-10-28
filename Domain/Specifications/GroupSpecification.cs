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

		public GroupSpecification(int skip, int take)
			: this()
		{
			Query.Skip(skip).Take(take);
		}

		public GroupSpecification(string group)
			:this()
		{
			FilterGroups(group);
		}

		public GroupSpecification(int skip, int take, string group)
			: this(skip, take)
		{
			FilterGroups(group);			
		}

		private void FilterGroups(string group)
		{
			Query.Where(g => group == null || g.Name.Contains(group));
		}
	}
}