using System;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services.External.Particular
{
	public class GroupingService : IGroupingService
	{
		readonly IAsyncRepository<Group, Guid> _groupRepository;
		readonly IAsyncRepository<Student, Guid> _studentRepository;

		public GroupingService(IAsyncRepository<Group, Guid> groupRepository, 
			IAsyncRepository<Student, Guid> studentRepository)
		{
			_groupRepository = groupRepository;
			_studentRepository = studentRepository;
		}

		public async Task<bool> IncludeStudentAsync(Guid studentId, Guid groupId)
		{
			var student = await _studentRepository.GetById(studentId);
			if (student is null)
				return false;

			var group = await _groupRepository.FirstOrDefault(groupId, new GroupSpecification());
			if (group is null)
				return false;

			var included = group.IncludeStudent(student);
			await _groupRepository.UpdateAsync(group);

			return included;
		}

		public async Task<bool> ExcludeStudentAsync(Guid studentId, Guid groupId)
		{
			var student = await _studentRepository.GetById(studentId);
			if (student is null)
				return false;

			var group = await _groupRepository.FirstOrDefault(groupId, new GroupSpecification());
			if (group is null)
				return false;

			var included = group.ExcludeStudent(student);
			await _groupRepository.UpdateAsync(group);

			return included;
		}
	}
}
