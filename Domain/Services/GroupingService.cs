using System;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;

namespace Velvetech.Domain.Services
{
	public class GroupingService : IGroupingService
	{

		IAsyncRepository<Group, Guid> _groupRepository;

		public GroupingService(IAsyncRepository<Group, Guid> groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<bool> IncludeStudentAsync(Guid studentId, Guid groupId)
		{
			var group = await _groupRepository.GetByIdAsync(groupId);

			var included = group.IncludeStudent(studentId);
			await _groupRepository.UpdateAsync(group);

			return included;
		}

		public async Task<bool> ExcludeStudentAsync(Guid studentId, Guid groupId)
		{
			var group = await _groupRepository.GetByIdAsync(groupId);

			var included = group.ExcludeStudent(studentId);
			await _groupRepository.UpdateAsync(group);

			return included;
		}
	}
}
