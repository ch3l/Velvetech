using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Domain.Common;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using System.Transactions;

namespace Domain.Services
{
	public class GroupingService : IGroupingService
	{
		
		IAsyncRepository<Student, Guid> _studentRepository;
		IAsyncRepository<Group, Guid> _groupRepository;

		public GroupingService(IAsyncRepository<Group, Guid> groupRepository, IAsyncRepository<Student, Guid> studentRepository
			//IAsyncRepository<Grouping> groupingRepository
			)
		{
			_groupRepository = groupRepository;
			_studentRepository = studentRepository;
			//_groupingRepository = groupingRepository;
		}

		public async Task IncludeStudentAsync(Guid studentId, Guid groupId)
		{
			var student = await _studentRepository.GetByIdAsync(studentId);
			if (student is null)
				return;

			var group = await _groupRepository.GetByIdAsync(groupId);
			if (group is null)
				return;
			
			
		}

		public Task ExcludeStudentAsync(Guid studentId, Guid groupId)
		{
			throw new NotImplementedException();
		}

		public Task OnStudentDeleteAsync(Guid studentId)
		{
			throw new NotImplementedException();
		}

		public Task OnGroupDeleteAsync(Guid groupId)
		{
			throw new NotImplementedException();
		}
	}
}
