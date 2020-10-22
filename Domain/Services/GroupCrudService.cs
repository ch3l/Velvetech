using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Common;
using Domain.Services.Interfaces;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;

namespace Domain.Services
{
	public class GroupCrudService : ICrudService<Group, Guid>
	{
		IAsyncRepository<Group, Guid> _groupRepository;
		IGroupingService _groupingService;

		public GroupCrudService(IAsyncRepository<Group, Guid> groupRepository, 
			IGroupingService studentGroupingService)
		{
			_groupRepository = groupRepository;
			_groupingService = studentGroupingService;
		}

		public IAsyncEnumerable<Group> GetAllAsync() =>
			_groupRepository.GetAllAsync();

		public IAsyncEnumerable<Group> GetRangeAsync(int skip, int take) =>
			_groupRepository.GetRangeAsync(skip, take);

		public async Task<Group> GetByIdAsync(Guid id) =>
			await _groupRepository.GetByIdAsync(id);

		public async Task<Group> AddAsync(Group entity) =>
			await _groupRepository.AddAsync(entity);

		public async Task UpdateAsync(Group entity) =>
			await _groupRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var entry = await _groupRepository.GetByIdAsync(id);
			if (entry is null)
				return;

			await _groupingService.OnGroupDeleteAsync(entry.Id);
			await _groupRepository.RemoveAsync(entry);
		}

		public async Task<int> CountAsync() =>
			await _groupRepository.CountAsync();
	}
}
