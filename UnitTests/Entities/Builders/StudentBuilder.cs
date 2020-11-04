using System;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.Internal;

namespace Velvetech.UnitTests.Entities.Builders
{
	class StudentBuilder
	{
		public async Task<Student> Build(IAsyncRepository<Student, Guid> repository, int index)
		{
			var student = new Student();
			var validator = new StudentValidator(new StudentValidationService(repository));

			student = await repository.AddAsync(student);
			student.SelectValidator(validator);
			student.SetFirstname($"Firstname {index}");
			student.SetMiddlename($"Middlename {index}");
			student.SetLastname($"Lastname {index}");
			await student.SetCallsignAsync($"Callsign {index}");
			await repository.UpdateAsync(student);

			return student;
		}
	}
}
