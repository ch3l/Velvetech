using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Common
{
	public interface IGroupingService
	{
		Task<bool> IncludeStudentAsync(Guid studentId, Guid groupId);
		Task<bool> ExcludeStudentAsync(Guid studentId, Guid groupId);
	}
}
