using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services
{
	public class StudentCrudService : ICrudService<Student, Guid>
	{
		readonly IAsyncRepository<Student, Guid> _studentRepository;

		public StudentCrudService(IAsyncRepository<Student, Guid> studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public IAsyncEnumerable<Student> GetAllAsync() =>
			_studentRepository.GetAllAsync(new StudentSpecification());

		public IAsyncEnumerable<Student> GetAllAsync(IFilter<Student> filter, ISpecification<Student> specification) => 
			_studentRepository.GetAllAsync(filter, specification);


		public IAsyncEnumerable<Student> GetRangeAsync(int skip, int take) =>
			_studentRepository.GetRangeAsync(skip, take, new StudentSpecification());

		public IAsyncEnumerable<Student> GetRangeAsync(int skip, int take, IFilter<Student> filter,
			ISpecification<Student> specification) => 
			_studentRepository.GetRangeAsync(skip, take, filter, specification);


		public async Task<Student> GetByIdAsync(Guid id) =>
			await _studentRepository.FirstOrDefault(id, new StudentSpecification());

		public async Task<Student> GetByIdAsync(Guid id, IFilter<Student> filter) => 
			await _studentRepository.FirstOrDefault(id, new StudentSpecification());


		public async Task<int> CountAsync() =>
			await _studentRepository.CountAsync();

		public async Task<int> CountAsync(IFilter<Student> filter) =>
			await _studentRepository.CountAsync(filter);


		public async Task<Student> AddAsync(Student entity) =>
			await _studentRepository.AddAsync(entity);

		public async Task UpdateAsync(Student entity) =>
			await _studentRepository.UpdateAsync(entity);

		public async Task DeleteAsync(Guid id)
		{
			var student= await _studentRepository.FirstOrDefault(id, new StudentSpecification());
			if (student is null)
				return;

			if (student.ExcludeFromAllGroups())
				await _studentRepository.UpdateAsync(student);

			await _studentRepository.RemoveAsync(student);
		}
	}
}
