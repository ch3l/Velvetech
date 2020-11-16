using System;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Base;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services
{
	public class StudentCrudService : CrudService<Student, Guid>
	{
		public StudentCrudService(IAsyncRepository<Student, Guid> studentRepository)
			:base(studentRepository)
		{
		}

		public override async Task DeleteAsync(Guid id)
		{
			var student= await _repository.FirstOrDefault(id, new StudentSpecification());
			if (student is null)
				return;

			if (student.ExcludeFromAllGroups())
				await _repository.UpdateAsync(student);

			await _repository.RemoveAsync(student);
		}
	}
}
