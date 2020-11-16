using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;

namespace Velvetech.Api.Controllers
{
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
		[HttpGet]
		public async Task<ActionResult<StudentDto[]>> ListIncludedAsync([FromQuery] IncludedStudentsRequest request)
		{
			var filter = new IncludedStudentsSpecification(request.GroupId);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		// GET: api/StudentGroup/ListNotIncluded
		[HttpGet]
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
		[HttpPost]
		public async Task<IActionResult> IncludeStudentAsync(StudentGroupRequest request)
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
		[HttpPost]
		public async Task<IActionResult> ExcludeStudentAsync(StudentGroupRequest request)
		{
			if (await _groupingService.ExcludeStudentAsync(request.StudentId, request.GroupId))
				return Ok();

			return NotFound();
		}
	}
}