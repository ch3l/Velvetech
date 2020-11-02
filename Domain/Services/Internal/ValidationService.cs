using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.Domain.Services.Internal
{
	class StudentValidationService: IValidationService<string>
	{
		private IAsyncRepository<Student, Guid> _repository;
		
		public StudentValidationService(IAsyncRepository<Student, Guid> repository)
		{
			_repository = repository;
		}

		public bool IsValueAlreadyExists(string value)
		{
			if (value == null)
				return false;

			return _repository.FirstOrDefault(student => student.Callsign.Equals(value)) != null;
		}
	}
}
