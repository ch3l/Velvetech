using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Entities;

namespace Domain.Common
{
	public interface IGroupingService
	{
		Task IncludeStudentAsync(Guid studentId, Guid groupId);
		Task ExcludeStudentAsync(Guid studentId, Guid groupId);
		
		Task OnStudentDeleteAsync(Guid studentId);
		Task OnGroupDeleteAsync(Guid groupId);
	}
}
