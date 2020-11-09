using System;
using System.Linq;
using System.Threading.Tasks;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Web.Services.Results;
using GroupDto = Velvetech.Web.Dtos.GroupDto;
using StudentGroupRequest = Velvetech.Web.Requests.StudentGroupRequest;

namespace Velvetech.Web.Services
{
	public class GroupService
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;
		private readonly IGroupingService _groupingService;

		public GroupService(ICrudService<Group, Guid> groupCrudService, IGroupingService groupingService)
		{
			_groupCrudService = groupCrudService;
			_groupingService = groupingService;
		}

		public async Task<GroupDto[]> ListAsync(string group) => await _groupCrudService.ListAsync(new GroupSpecification(group))
				.Select(Extensions.ToDto)
				.ToArrayAsync();

		public async Task<EntityActionResult> AddAsync(GroupDto dto)
		{
			var validator = new GroupValidator();
			var entry = Group.Build(validator, dto.Name);

			if (entry.HasErrors)
				return new GroupErrors(entry.ErrorsStrings);

			entry = await _groupCrudService.AddAsync(entry);
			return new SuccessfulEntityAction<GroupDto>(entry.ToDto());
		}

		public async Task<EntityActionResult> UpdateAsync(GroupDto dto)
		{
			var entry = await _groupCrudService.GetByIdAsync(dto.Id);
			if (entry is null)
				return new EntityNotFound();

			if (!entry.HasValidator)
			{
				var validator = new GroupValidator();
				entry.SelectValidator(validator);
			}

			entry.SetName(dto.Name);

			if (entry.HasErrors)
				return new GroupErrors(entry.ErrorsStrings);

			await _groupCrudService.UpdateAsync(entry);
			return new SuccessfulEntityAction<GroupDto>(entry.ToDto());
		}
		public async Task DeleteAsync(Guid id) => await _groupCrudService.DeleteAsync(id);

		public async Task<bool> IncludeStudentAsync(StudentGroupRequest request)
		{
			var includeResult = await _groupingService.IncludeStudentAsync(request.StudentId, request.GroupId);

			if (includeResult)
				return true; // Ok();
			else
				return false;// Ok("Already included");
		}

		public async Task<bool> ExcludeStudentAsync(StudentGroupRequest request)
		{
			if (await _groupingService.ExcludeStudentAsync(request.StudentId, request.GroupId))
				return true;

			return false;
		}
	}
}


