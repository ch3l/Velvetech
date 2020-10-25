using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;
using Velvetech.Presentation.Server.Filtering;
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
			var filter = new GroupFilter(group);
			return await _groupCrudService.GetAllAsync(filter)
				.Select(Extensions.ToDto)
				.ToArrayAsync();
		}

		// PUT: api/Groups/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<IActionResult> AddAsync(GroupDto dto)
		{
			var group = dto.FromDto();

			try
			{
				await _groupCrudService.AddAsync(group);
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return Ok("Updated successfully");
		}


		// PUT: api/Groups/Update
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut]
		public async Task<IActionResult> UpdateAsync(GroupDto dto)
		{
			var item = dto.FromDto();

			try
			{
				await _groupCrudService.UpdateAsync(item);
			}
			catch (Exception)
			{
				if ((await _groupCrudService.GetByIdAsync(item.Id)) is null)
					return NotFound();
				else
					throw;
			}

			return Ok("Updated successfully");
		}

		// PUT: api/Groups/AddStudent
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<IActionResult> IncludeStudentAsync(StudentGroupRequest request)
		{
			if (await _groupingService.IncludeStudentAsync(request.StudentId, request.GroupId))
				return Ok();

			return Content("Already included");
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
