using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;

namespace Velvetech.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StudentGroupController : ControllerBase
	{
		private readonly ICrudService<Student, Guid> _studentCrudService;
		private readonly IGroupingService _groupingService;

		public StudentGroupController(
			ICrudService<Student, Guid> studentCrudService,
			IGroupingService groupingService)
		{
			_studentCrudService = studentCrudService;
			_groupingService = groupingService;
		}

		// GET: api/StudentGroup/ListIncluded 
		[Authorize(Roles = 
			Shared.Authentication.Constants.Roles.StudentRead + "," + 
			Shared.Authentication.Constants.Roles.GroupRead + "," +
			Shared.Authentication.Constants.Roles.StudentGroupRead)]
		[HttpGet]
		[SwaggerOperation(
			Summary = "List included into Group Students",
			Description = "List included into Group Students",
			OperationId = "StudentGroupController.ListIncludedAsync",
			Tags = new[]
			{
				"Controller: StudentGroup",
				//"Action: ListIncludedAsync",
			})]
		public async Task<ActionResult<StudentDto[]>> ListIncludedAsync([FromQuery] IncludedStudentsRequest request)
		{
			var filter = new IncludedStudentsSpecification(request.GroupId);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		// GET: api/StudentGroup/ListNotIncluded
		[Authorize(Roles =
			Shared.Authentication.Constants.Roles.StudentRead + "," +
			Shared.Authentication.Constants.Roles.GroupRead + "," +
			Shared.Authentication.Constants.Roles.StudentGroupRead)]
		[HttpGet]
		[SwaggerOperation(
			Summary = "List not included into Group Students",
			Description = "List not included into Group Students",
			OperationId = "StudentGroupController.ListNotIncludedAsync",
			Tags = new[]
			{
				"Controller: StudentGroup",
				//"Action: ListNotIncludedAsync",
			})]
		public async Task<ActionResult<StudentDto[]>> ListNotIncludedAsync([FromQuery] IncludedStudentsRequest request)
		{
			var filter = new NotIncludedStudentsSpecification(request.GroupId);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		// PUT: api/StudentGroup/IncludeStudentAsync
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		// GET: api/StudentGroup/ListIncluded 
		[Authorize(Roles =
			Shared.Authentication.Constants.Roles.StudentRead + "," +
			Shared.Authentication.Constants.Roles.GroupRead + "," +
			Shared.Authentication.Constants.Roles.StudentGroupCrud)]
		[HttpPost]
		[SwaggerOperation(
			Summary = "Includes Student to group",
			Description = "Includes Student to group by UserId and GroupId",
			OperationId = "StudentGroupController.IncludeStudentAsync",
			Tags = new[]
			{
				"Controller: StudentGroup",
				//"Action: IncludeStudent",
				//"Includes\\Excludes Student(s) to\\from group"
			})]
		public async Task<ActionResult> IncludeStudentAsync(StudentGroupRequest request)
		{
			var includeResult = await _groupingService.IncludeStudentAsync(request.StudentId, request.GroupId);

			if (includeResult)
				return Ok();
			else
				return Ok("Already included");
		}

		// PUT: api/StudentGroup/ExcludeStudentAsync
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[Authorize(Roles =
			Shared.Authentication.Constants.Roles.StudentRead + "," +
			Shared.Authentication.Constants.Roles.GroupRead + "," +
			Shared.Authentication.Constants.Roles.StudentGroupCrud)]
		[HttpPost]
		[SwaggerOperation(
			Summary = "Excludes Student from group",
			Description = "Excludes Student from group by UserId and GroupId",
			OperationId = "StudentGroupController.ExcludeStudentAsync",
			Tags = new[]
			{
				"Controller: StudentGroup", 
				//"Action: ExcludeStudent", 
				//"Includes\\Excludes Student(s) to\\from group"
			})]
		public async Task<ActionResult> ExcludeStudentAsync(StudentGroupRequest request)
		{
			if (await _groupingService.ExcludeStudentAsync(request.StudentId, request.GroupId))
				return Ok();

			return NotFound();
		}
	}
}