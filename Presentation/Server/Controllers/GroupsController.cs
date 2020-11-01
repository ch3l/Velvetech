using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Presentation.Shared.Dtos;
using Velvetech.Presentation.Shared.Requests;

namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GroupsController : ControllerBase
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;
		private readonly IGroupingService _groupingService;

		public GroupsController(ICrudService<Group, Guid> groupCrudService, IGroupingService groupingService)
		{
			_groupCrudService = groupCrudService;
			_groupingService = groupingService;
		}

		// GET: api/Groups/List
		[HttpGet]
		public async Task<GroupDto[]> ListAsync(string group)
		{
			return await _groupCrudService.ListAsync(new GroupSpecification(group))
				.Select(Extensions.ToDto)
				.ToArrayAsync();
		}

		// PUT: api/Groups/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Group>> AddAsync(GroupDto dto)
		{
			var entry = new Group();

			entry.SetName(dto.Name);

			if (entry.HasValidationErrors)
				return BadRequest(entry.Errors);

			entry = await _groupCrudService.AddAsync(entry);
			return Ok(entry);
		}


		// PUT: api/Groups/Update
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut]
		public async Task<ActionResult<Group>> UpdateAsync(GroupDto dto)
		{
			var entry = await _groupCrudService.GetByIdAsync(dto.Id);
			if (entry is null)
				return NotFound();

			entry.SetName(dto.Name);

			if (entry.HasValidationErrors)
				return BadRequest(entry.Errors);

			await _groupCrudService.UpdateAsync(entry);
			return Ok(entry);
		}

		// PUT: api/Groups/AddStudent
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<IActionResult> IncludeStudentAsync(StudentGroupRequest request)
		{
			if (await _groupingService.IncludeStudentAsync(request.StudentId, request.GroupId))
				return Ok();

			return Ok("Already included");
		}

		// PUT: api/Groups/AddStudent
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<IActionResult> ExcludeStudentAsync(StudentGroupRequest request)
		{
			if (await _groupingService.ExcludeStudentAsync(request.StudentId, request.GroupId))
				return Ok();

			return NotFound();
		}

		// DELETE: api/Groups/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAsync(Guid id)
		{
			await _groupCrudService.DeleteAsync(id);
			return Ok();
		}
	}
}
