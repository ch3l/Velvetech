using System;
using System.Threading.Tasks;

namespace Velvetech.Domain.Services.External.Interfaces
{
	public interface IGroupingService
	{
		Task<bool> IncludeStudentAsync(Guid studentId, Guid groupId);
		Task<bool> ExcludeStudentAsync(Guid studentId, Guid groupId);
	}
}
