using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Services.Internal.Interfaces;
using Velvetech.UnitTests.Repository;

namespace Velvetech.UnitTests.Services
{
	class FakeStudentValidationService : IStudentValidationService
	{
		private FakeStudentRepository _repository;

		public FakeStudentValidationService(FakeStudentRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> CallsignExistsAsync(string value)
		{
			if (value is null)
				return await Task.FromResult(false);

			return await Task.FromResult(_repository.FirstOrDefault(x => x.Callsign == value) != null);
		}
	}
}
