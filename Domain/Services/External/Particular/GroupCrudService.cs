using System;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services.External.Particular
{
	public class GroupCrudService : CrudService<Group, Guid>
	{
		public GroupCrudService(IAsyncRepository<Group, Guid> groupRepository)
			:base (groupRepository)
		{
		}

		public override async Task DeleteAsync(Guid id)
		{
			var group = await _repository.FirstOrDefault(id, new GroupSpecification());
			if (group is null)
				return;

			if (group.ExcludeAllStudents())
				await _repository.UpdateAsync(group);

			await _repository.RemoveAsync(group);
		}
	}
}
