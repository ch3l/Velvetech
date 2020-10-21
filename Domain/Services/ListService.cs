using System;
using System.Threading.Tasks;

using Domain.Common;
using Domain.Services.Interfaces;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;

namespace Domain.Services
{
	public class ListService : IListService<Student>
	{
		IAsyncRepository<Student> _studentRepository;
		IGroupingService _groupingService;

		public ListService(IAsyncRepository<Student> studentRepository, IGroupingService groupingService)
		{
			_studentRepository = studentRepository;
			_groupingService = groupingService;
		}

		public async Task<Student[]> GetAllAsync() =>
			await _studentRepository.GetAllAsync();

		public async Task<Student[]> GetRangeAsync(int skip, int take) =>
			await _studentRepository.GetRangeAsync(skip, take);

		public async Task<Student> GetByIdAsync(Guid id) =>
			await _studentRepository.GetByIdAsync(id);

		public async Task<Student> AddAsync(Student entity) =>
			await _studentRepository.AddAsync(entity);

		public async Task UpdateAsync(Student entity) =>
			await _studentRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _studentRepository.GetByIdAsync(id);
			if (entity is null)
				return;

			await _studentRepository.DeleteAsync(entity);
			await _groupingService.OnStudentDelete(entity.Id);
		}

		public async Task<int> CountAsync() =>
			await _studentRepository.CountAsync();

	}
}
