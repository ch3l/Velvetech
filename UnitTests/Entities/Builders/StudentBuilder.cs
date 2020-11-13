using System;
using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.Domain.Services.Internal;

namespace Velvetech.UnitTests.Entities.Builders
{
	static class StudentBuilder
	{
		public static async Task<Student> BuildAsync(IAsyncRepository<Student, Guid> repository, int index)
		{
			var validator = new StudentValidator(new StudentValidationService(repository));
			var student = await Student.BuildAsync(validator,
				(index % 2) + 1,
				$"Firstname {index}",
				$"Middlename {index}",
				$"Lastname {index}",
				$"Callsign {index}");

			student = await repository.AddAsync(student);

			return student;
		}
	}
}
