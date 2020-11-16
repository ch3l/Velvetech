using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Services.Internal.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;

namespace Velvetech.Api.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly ICrudService<Student, Guid> _studentCrudService;
		private readonly IStudentValidationService _studentValidationService;

		public StudentController(
			ICrudService<Student, Guid> studentCrudService,
			IStudentValidationService studentValidationService)
		{
			_studentCrudService = studentCrudService;
			_studentValidationService = studentValidationService;
		}

		// GET: api/Student/Students/List
		[Authorize(Roles = 
			Shared.Authentication.Constants.Roles.StudentRead + "," +
			Shared.Authentication.Constants.Roles.GroupRead)]
		[HttpGet]
		public async Task<ActionResult<Page<StudentDto>>> ListAsync([FromQuery] StudentFilterPagedRequest request)
		{
			var pageSize = request.PageSize ?? 10;
			var pageIndex = request.PageIndex ?? 0;

			if (pageSize < 10)
				pageSize = 10;

			var totalItems = await _studentCrudService.CountAsync(
				new StudentSpecification(
					request.Sex,
					request.Fullname,
					request.Callsign,
					request.Group));

			var lastPageIndex = totalItems / pageSize;

			//if (totalItems == pageSize * lastPageIndex)
			//	lastPageIndex--;

			if (pageIndex > lastPageIndex)
				pageIndex = lastPageIndex;

			if (pageIndex < 0)
				pageIndex = 0;

			var filter = new StudentSpecification(
				skip: pageSize * pageIndex,
				take: pageSize,
				sex: request.Sex,
				fullname: request.Fullname,
				callsign: request.Callsign,
				@group: request.Group);

			var students = await _studentCrudService.ListAsync(filter)
				.Select(DtoExtensions.ToDto)
				.ToArrayAsync();

			return new Page<StudentDto>
			{
				IsLastPage = pageIndex == lastPageIndex,
				PageIndex = pageIndex,
				PageSize = pageSize,
				Items = students,
			};
		}

		// GET: api/Student/Get
		[Authorize(Roles = Shared.Authentication.Constants.Roles.StudentRead)]
		[HttpGet("{id}")]
		public async Task<ActionResult<StudentDto>> GetAsync(Guid? id)
		{
			if (id is null)
				return BadRequest("Corrupted Id");

			return (await _studentCrudService.GetByIdAsync(id.Value)).ToDto();
		}

		// GET: api/Student/Count
		[Authorize(Roles = Shared.Authentication.Constants.Roles.StudentRead)]
		[HttpGet]
		public async Task<ActionResult<int>> CountAsync() => 
			await _studentCrudService.CountAsync();

		// PUT: api/Students/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[Authorize(Roles = Shared.Authentication.Constants.Roles.StudentCrud)]
		[HttpPost]
		public async Task<ActionResult<StudentDto>> AddAsync(StudentDto dto)
		{
			var validator = new StudentValidator(_studentValidationService);
			var entry = await Student.BuildAsync(validator, dto.SexId, dto.Firstname, dto.Middlename, dto.Lastname, dto.Callsign);

			if (entry.HasErrors)
				return BadRequest(entry.ErrorsStrings);

			entry = await _studentCrudService.AddAsync(entry);
			return Ok(entry);
		}

		// PUT: api/Student/Update
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[Authorize(Roles = Shared.Authentication.Constants.Roles.StudentCrud)]
		[HttpPut]
		public async Task<ActionResult<StudentDto>> UpdateAsync(StudentDto dto)
		{
			var entry = await _studentCrudService.GetByIdAsync(dto.Id);
			if (entry is null)
				return NotFound();

			var validator = new StudentValidator(_studentValidationService);
			entry.SelectValidator(validator);

			entry.SetFirstname(dto.Firstname);
			entry.SetMiddlename(dto.Middlename);
			entry.SetLastname(dto.Lastname);
			await entry.SetCallsignAsync(dto.Callsign);
			entry.SetSexId(dto.SexId);

			if (entry.HasErrors)
				return BadRequest(entry.ErrorsStrings);

			await _studentCrudService.UpdateAsync(entry);
			return Ok(entry.ToDto());
		}

		// DELETE: api/Students/Delete/{GUID}
		[Authorize(Roles = 
			Shared.Authentication.Constants.Roles.StudentCrud + "," +
			Shared.Authentication.Constants.Roles.StudentGroupCrud)]
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAsync(Guid id)
		{
			await _studentCrudService.DeleteAsync(id);
			return Ok();
		}
	}
}
