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
	public class StudentCrudService : ICrudService<Student, Guid>
	{
		readonly IAsyncRepository<Student, Guid> _studentRepository;

		public StudentCrudService(IAsyncRepository<Student, Guid> studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public async Task<Student> GetByIdAsync(Guid id) =>
			await _studentRepository.GetById(id);

		public async Task<Student> FirstOrDefault(Guid id, ISpecification<Student> specification) =>
			await _studentRepository.FirstOrDefault(id, specification);


		public IAsyncEnumerable<Student> ListAsync() =>
			_studentRepository.ListAsync();

		public IAsyncEnumerable<Student> ListAsync(ISpecification<Student> specification) => 
			_studentRepository.ListAsync(specification);


		public async Task<int> CountAsync() =>
			await _studentRepository.CountAsync();

		public async Task<int> CountAsync(ISpecification<Student> specification) =>
			await _studentRepository.CountAsync(specification);


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
