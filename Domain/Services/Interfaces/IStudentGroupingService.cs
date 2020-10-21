using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Entities;

namespace Domain.Common
{
	public interface IGroupingService
	{
		Task IncludeStudent(Guid groupId, Guid studentId);
		Task ExcludeStudent(Guid groupId, Guid studentId);

		Task OnStudentDelete(Guid studentId);
		Task OnGroupDelete(Guid groupId);
	}
}
