using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services.External
{
	public class GroupCrudService : ICrudService<Group, Guid>
	{
		readonly IAsyncRepository<Group, Guid> _groupRepository;

		public GroupCrudService(IAsyncRepository<Group, Guid> groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<Group> GetByIdAsync(Guid id) =>
			await _groupRepository.GetById(id);

		public async Task<Group> FirstOrDefault(Guid id, ISpecification<Group> specification) =>
			await _groupRepository.FirstOrDefault(id, specification);


		public IAsyncEnumerable<Group> ListAsync() =>
			_groupRepository.ListAsync();

		public IAsyncEnumerable<Group> ListAsync(ISpecification<Group> specification) => 
			_groupRepository.ListAsync(specification);

		
		public async Task<int> CountAsync() =>
			await _groupRepository.CountAsync();

		public async Task<int> CountAsync(ISpecification<Group> specification) =>
			await _groupRepository.CountAsync(specification);


		public async Task<Group> AddAsync(Group entity) =>
			await _groupRepository.AddAsync(entity);

		public async Task UpdateAsync(Group entity) =>
			await _groupRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var group = await _groupRepository.FirstOrDefault(id, new GroupSpecification());
			if (group is null)
				return;

			if (group.ExcludeAllStudents())
				await _groupRepository.UpdateAsync(group);

			await _groupRepository.RemoveAsync(group);
		}
	}
}
