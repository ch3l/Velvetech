using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;

namespace Velvetech.Domain.Services
{
	public class GroupCrudService : ICrudService<Group, Guid>
	{
		readonly IAsyncRepository<Group, Guid> _groupRepository;

		public GroupCrudService(IAsyncRepository<Group, Guid> groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public IAsyncEnumerable<Group> GetAllAsync() =>
			_groupRepository.GetAllAsync();

		public IAsyncEnumerable<Group> GetAllAsync(IFilter<Group> filter, ISpecification<Group> specification) => 
			_groupRepository.GetAllAsync(filter, specification);


		public IAsyncEnumerable<Group> GetRangeAsync(int skip, int take) =>
			_groupRepository.GetRangeAsync(skip, take);

		public IAsyncEnumerable<Group> GetRangeAsync(int skip, int take, IFilter<Group> filter,
			ISpecification<Group> specification) => 
			_groupRepository.GetRangeAsync(skip, take, filter, specification);

		public async Task<Group> GetByIdAsync(Guid id) =>
			await _groupRepository.GetByIdAsync(id);

		public async Task<Group> GetByIdAsync(Guid id, IFilter<Group> filter) => 
			await _groupRepository.GetByIdAsync(id, filter);

		
		public async Task<int> CountAsync() =>
			await _groupRepository.CountAsync();

		public async Task<int> CountAsync(IFilter<Group> filter) =>
			await _groupRepository.CountAsync(filter);


		public async Task<Group> AddAsync(Group entity) =>
			await _groupRepository.AddAsync(entity);

		public async Task UpdateAsync(Group entity) =>
			await _groupRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var group = await _groupRepository.GetByIdAsync(id);
			if (group is null)
				return;

			if (group.ExcludeAllStudents())
				await _groupRepository.UpdateAsync(group);

			await _groupRepository.RemoveAsync(group);
		}
	}
}
