using System;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services.External
{
	public class GroupingService : IGroupingService
	{
		readonly IAsyncRepository<Group, Guid> _groupRepository;

		public GroupingService(IAsyncRepository<Group, Guid> groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<bool> IncludeStudentAsync(Guid studentId, Guid groupId)
		{
			var group = await _groupRepository.FirstOrDefault(groupId, new GroupSpecification());

			var included = group.IncludeStudent(studentId);
			await _groupRepository.UpdateAsync(group);

			return included;
		}

		public async Task<bool> ExcludeStudentAsync(Guid studentId, Guid groupId)
		{
			var group = await _groupRepository.FirstOrDefault(groupId, new GroupSpecification());

			var included = group.ExcludeStudent(studentId);
			await _groupRepository.UpdateAsync(group);

			return included;
		}
	}
}
