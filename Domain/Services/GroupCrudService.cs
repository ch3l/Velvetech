using System;
using System.Collections.Generic;
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
		IAsyncRepository<Group> _groupRepository;
		IGroupingService _studentGroupingService;

		public GroupCrudService(IAsyncRepository<Group> groupRepository, 
			IGroupingService studentGroupingService)
		{
			_groupRepository = groupRepository;
			_studentGroupingService = studentGroupingService;
		}

		public async Task<Group[]> GetAllAsync() =>
			await _groupRepository.GetAllAsync();

		public async Task<Group[]> GetRangeAsync(int skip, int take) =>
			await _groupRepository.GetRangeAsync(skip, take);

		public async Task<Group> GetByIdAsync(Guid id) =>
			await _groupRepository.GetByIdAsync(id);

		public async Task<Group> AddAsync(Group entity) =>
			await _groupRepository.AddAsync(entity);

		public async Task UpdateAsync(Group entity) =>
			await _groupRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _groupRepository.GetByIdAsync(id);
			if (entity is null)
				return;

			await _groupRepository.DeleteAsync(entity);
			await _studentGroupingService.OnGroupDelete(entity.Id);
		}

		public async Task<int> CountAsync() =>
			await _groupRepository.CountAsync();
	}
}
