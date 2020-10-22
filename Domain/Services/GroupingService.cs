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
		
		//IAsyncRepository<Grouping> _groupingRepository;

		public GroupingService(
			//IAsyncRepository<Grouping> groupingRepository
			)
		{
			//_groupingRepository = groupingRepository;
		}

		public async Task IncludeStudentAsync(Guid groupId, Guid studentId) => throw new NotImplementedException();
		public async Task ExcludeStudentAsync(Guid groupId, Guid studentId) => throw new NotImplementedException();

		public async Task OnStudentDeleteAsync(Guid studentId)
		{
			//var entriesToDelete = _groupingRepository
			//	.GetEntity()
			//	.Where(grouping => grouping.StudentId == studentId)
			//	.ToArray();

			//await _groupingRepository.RemoveRangeAsync(entriesToDelete);
		}

		public async Task OnGroupDeleteAsync(Guid groupId)
		{
			//var entriesToDelete = _groupingRepository
			//	.GetEntity()
			//	.Where(grouping => grouping.GroupId == groupId)
			//	.ToArray();

			//await _groupingRepository.RemoveRangeAsync(entriesToDelete);
		}
	}
}
