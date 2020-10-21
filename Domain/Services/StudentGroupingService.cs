using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Domain.Common;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;

namespace Domain.Services
{
	public class GroupingService : IGroupingService
	{
		
		IAsyncRepository<Grouping> _groupingRepository;

		public GroupingService(IAsyncRepository<Grouping> groupingRepository)
		{
			_groupingRepository = groupingRepository;
		}

		public Task IncludeStudent(Guid groupId, Guid studentId) => throw new NotImplementedException();
		public Task ExcludeStudent(Guid groupId, Guid studentId) => throw new NotImplementedException();
		public Task OnStudentDelete(Guid studentId) => throw new NotImplementedException();
		public Task OnGroupDelete(Guid groupId) => throw new NotImplementedException();
	}
}
