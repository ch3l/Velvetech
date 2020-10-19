using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Entities.StudentAggregate;
using Velvetech.Domain.Entities.GroupAggregate;

namespace Domain.Common
{
	public interface IStudentGroupingService
	{
		Task IncludeStudent(Guid groupId, Guid studentId);
		Task ExcludeStudent(Guid groupId, Guid studentId);

		Task OnStudentDelete(Guid studentId);
		Task OnGroupDelete(Guid groupId);
	}
}
