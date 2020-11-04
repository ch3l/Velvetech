using System;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.Domain.Services.Internal
{
	public class StudentValidationService : IStudentValidationService
	{
		private readonly IAsyncRepository<Student, Guid> _repository;

		public StudentValidationService(IAsyncRepository<Student, Guid> repository)
		{
			_repository = repository;
		}

		public async Task<bool> CallsignExistsAsync(string value)
		{
			if (value == null)
				return false;

			var foundValue = await _repository
				.FirstOrDefault(student => 
					student.Callsign != null && 
					student.Callsign.Equals(value));

			return foundValue != null;
		}
	}
}
