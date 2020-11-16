using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;
		private readonly IGroupingService _groupingService;

		public GroupController(ICrudService<Group, Guid> groupCrudService, IGroupingService groupingService)
		{
			_groupCrudService = groupCrudService;
			_groupingService = groupingService;
		}

		// GET: api/Group/List
		[HttpGet]
		public async Task<GroupDto[]> ListAsync(string group) =>
			await _groupCrudService.ListAsync(new GroupSpecification(group))
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();

		// PUT: api/Groups/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Group>> AddAsync(GroupDto dto)
		{
			var validator = new GroupValidator();
			var entry = Group.Build(validator, dto.Name);

			if (entry.HasErrors)
				return BadRequest(entry.ErrorsStrings);

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

			var validator = new GroupValidator();
			entry.SelectValidator(validator);
			entry.SetName(dto.Name);

			if (entry.HasErrors)
				return BadRequest(entry.ErrorsStrings);

			await _groupCrudService.UpdateAsync(entry);
			return Ok(entry);
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
