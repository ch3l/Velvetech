using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.Domain.Services.Base.Interfaces;
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
		[SwaggerOperation(
			Summary = "Returns Students list",
			Description = "Returns Students list, supports:<br/><br/>" +
						  "complete match search by 'Sex' query tag;<br/>" +
						  "partial match search by 'Fullname' query tag;<br/>" +
						  "partial match search by 'Callsign' query tag;<br/>" +
						  "partial match search by 'Group' query tag;<br><br>"+
						  "Pagination by 'PageIndex' and 'PageSize' query tags.<br>",
			OperationId = "StudentController.ListAsync",
			Tags = new[]
			{
				"Controller: Student",
				//"Action: List"
			})]
		public async Task<ActionResult<Page<StudentDto>>> ListAsync([FromQuery] StudentFilterPagedRequest request)
		{
			var pageSize = request.PageSize ?? 10;
			var pageIndex = request.PageIndex ?? 0;

			if (pageSize < 10)
				pageSize = 10;

			var totalItems = await _studentCrudService.CountAsync(
				new StudentSpecification(
					sex: request.Sex,
					fullname: request.Fullname,
					callsign: request.Callsign,
					group: request.Group));

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
		[SwaggerOperation(
			Summary = "Returns Students by Id",
			Description = "Returns Students by Id",
			OperationId = "StudentController.GetAsync",
			Tags = new[]
			{
				"Controller: Student", 
				//"Action: List"
			})]
		public async Task<ActionResult<StudentDto>> GetAsync(Guid? id)
		{
			if (id is null)
				return BadRequest("Corrupted Id");

			return (await _studentCrudService.GetByIdAsync(id.Value)).ToDto();
		}

		// GET: api/Student/Count
		[Authorize(Roles = Shared.Authentication.Constants.Roles.StudentRead)]
		[HttpGet]
		[SwaggerOperation(
			Summary = "Counts Students",
			Description = "Counts Students",
			OperationId = "StudentController.CountAsync",
			Tags = new[]
			{
				"Controller: Student", 
				//"Action: Count"
			})]
		public async Task<ActionResult<int>> CountAsync() => 
			await _studentCrudService.CountAsync();

		// PUT: api/Students/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[Authorize(Roles = Shared.Authentication.Constants.Roles.StudentCrud)]
		[HttpPost]
		[SwaggerOperation(
			Summary = "Adds Student",
			Description = "Adds Student",
			OperationId = "StudentController.AddAsync",
			Tags = new[]
			{
				"Controller: Student", 
				//"Action: Add"
			})]
		public async Task<ActionResult<StudentDto>> AddAsync(StudentDto dto)
		{
			var validator = new DefaultStudentValidator(_studentValidationService);
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
		[SwaggerOperation(
			Summary = "Updates Student",
			Description = "Updates Student",
			OperationId = "StudentController.UpdateAsync",
			Tags = new[]
			{
				"Controller: Student", 
				//"Action: Update"
			})]
		public async Task<ActionResult<StudentDto>> UpdateAsync(StudentDto dto)
		{
			var student = await _studentCrudService.GetByIdAsync(dto.Id);
			if (student is null)
				return NotFound();

			var validator = new DefaultStudentValidator(_studentValidationService);
			student.SelectValidator(validator);

			student.SetFirstname(dto.Firstname);
			student.SetMiddlename(dto.Middlename);
			student.SetLastname(dto.Lastname);
			await student.SetCallsignAsync(dto.Callsign);
			student.SetSexId(dto.SexId);

			if (student.HasErrors)
				return BadRequest(student.ErrorsStrings);

			await _studentCrudService.UpdateAsync(student);
			return Ok(student.ToDto());
		}

		// DELETE: api/Students/Delete/{GUID}
		[Authorize(Roles = 
			Shared.Authentication.Constants.Roles.StudentCrud + "," +
			Shared.Authentication.Constants.Roles.StudentGroupCrud)]
		[HttpDelete("{id}")]
		[SwaggerOperation(
			Summary = "Deletes Student",
			Description = "Deletes Student by Id",
			OperationId = "StudentController.DeleteAsync",
			Tags = new[]
			{
				"Controller: Student", 
				//"Action: Delete", 
				//"Includes\\Excludes Student(s) to\\from group"
			})]
		public async Task<ActionResult> DeleteAsync(Guid id)
		{
			await _studentCrudService.DeleteAsync(id);
			return Ok();
		}
	}
}
