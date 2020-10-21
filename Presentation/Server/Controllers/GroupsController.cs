using System;
using System.Linq;
using System.Threading.Tasks;

using Domain.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Presentation.Shared.Dtos;

using Velvetech.Domain.Entities;

namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GroupsController : ControllerBase
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;

		public GroupsController(ICrudService<Group, Guid> groupCrudService)
		{
			_groupCrudService = groupCrudService;
		}

		// GET: api/Groups/List
		[HttpGet]
		public async Task<GroupDto[]> ListAsync()
		{
			return await _groupCrudService.GetAllAsync()
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

		// DELETE: api/Groups/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAsync(Guid id)
		{
			await _groupCrudService.DeleteAsync(id);
			return Ok();

			/*
			var student = await _context.Student.FindAsync(id);
			if (student == null)
			{
				return NotFound();
			}

			_context.Student.Remove(student);
			await _context.SaveChangesAsync();

			return student;
			*/
		}
	}
}
