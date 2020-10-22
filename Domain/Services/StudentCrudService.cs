using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;

namespace Velvetech.Domain.Services
{
	public class StudentCrudService : ICrudService<Student, Guid>
	{
		IAsyncRepository<Student, Guid> _studentRepository;

		public StudentCrudService(IAsyncRepository<Student, Guid> studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public IAsyncEnumerable<Student> GetAllAsync() =>
			_studentRepository.GetAllAsync();

		public IAsyncEnumerable<Student> GetRangeAsync(int skip, int take) =>
			_studentRepository.GetRangeAsync(skip, take);

		public async Task<Student> GetByIdAsync(Guid id) =>
			await _studentRepository.GetByIdAsync(id);

		public async Task<Student> AddAsync(Student entity) =>
			await _studentRepository.AddAsync(entity);

		public async Task UpdateAsync(Student entity) =>
			await _studentRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var student= await _studentRepository.GetByIdAsync(id);
			if (student is null)
				return;

			if (student.ExcludeFromAllGroups())
				await _studentRepository.UpdateAsync(student);

			await _studentRepository.RemoveAsync(student);
		}

		public async Task<int> CountAsync() =>
			await _studentRepository.CountAsync();
	}
}
