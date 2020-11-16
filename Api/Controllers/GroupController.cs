using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared.Dtos;

namespace Velvetech.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;

		public GroupController(ICrudService<Group, Guid> groupCrudService)
		{
			_groupCrudService = groupCrudService;
		}

		// GET: api/Group/List
		[Authorize(Roles = Shared.Authentication.Constants.Roles.GroupRead)]
		[HttpGet]
		[SwaggerOperation(
			Summary = "Returns Groups list",
			Description = @"Returns Groups list, support partial search by 'Group' query tag",
			OperationId = "GroupController.ListAsync",
			Tags = new[]
			{
				"Controller: Group", 
				//"Action: List"
			})]
		public async Task<ActionResult<GroupDto[]>> ListAsync(string group) =>
			await _groupCrudService.ListAsync(new GroupSpecification(group))
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();

		// PUT: api/Groups/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[Authorize(Roles = Shared.Authentication.Constants.Roles.GroupCrud)]
		[HttpPost]
		[SwaggerOperation(
			Summary = "Adds Group",
			Description = "Adds Group",
			OperationId = "GroupController.AddAsync",
			Tags = new[]
			{
				"Controller: Group", 
				//"Action: Add"
			})]
		public async Task<ActionResult<GroupDto>> AddAsync(GroupDto dto)
		{
			var validator = new GroupValidator();
			var entry = Group.Build(validator, dto.Name);

			if (entry.HasErrors)
				return BadRequest(entry.ErrorsStrings);

			entry = await _groupCrudService.AddAsync(entry);
			
			return Ok(entry.ToDto());
		}


		// PUT: api/Groups/Update
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[Authorize(Roles = Shared.Authentication.Constants.Roles.GroupCrud)]
		[HttpPut]
		[SwaggerOperation(
			Summary = "Updates Group",
			Description = "Updates Group",
			OperationId = "GroupController.UpdateAsync",
			Tags = new[]
			{
				"Controller: Group", 
				//"Action: Update"
			})]
		public async Task<ActionResult<GroupDto>> UpdateAsync(GroupDto dto)
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

			return Ok(entry.ToDto());
		}

		// DELETE: api/Groups/5
		[Authorize(Roles = Shared.Authentication.Constants.Roles.GroupCrud)]
		[HttpDelete("{id}")]
		[SwaggerOperation(
			Summary = "Deletes Group",
			Description = "Deletes Group by Id. Before deletion all Students excluded from Group if Group contains any",
			OperationId = "GroupController.DeleteAsync",
			Tags = new[]
			{
				"Controller: Group", 
				//"Action: Delete", 
				//"Includes\\Excludes Student(s) to\\from group"
			})]
		public async Task<ActionResult> DeleteAsync(Guid id)
		{
			await _groupCrudService.DeleteAsync(id);
			return Ok();
		}
	}
}
