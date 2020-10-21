using System;
using System.Collections.Generic;
using System.Linq;

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Domain.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Velvetech.Presentation.Server;
using Presentation.Shared.Dtos;
using Presentation.Shared.Requests;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;
using Domain.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Shared;

namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		ICrudService<Student, Guid> _studentCrudService;
		IGroupingService _groupingService;

		public StudentsController(ICrudService<Student, Guid> studentCrudService, IGroupingService groupingService)
		{
			_studentCrudService = studentCrudService;
			_groupingService = groupingService;
		}

		// GET: api/Test/Students
		[HttpGet]
		public async Task<ActionResult<Page<StudentDto>>> StudentsAsync(int pageSize = 10, int pageIndex = 0)
		{
			if (pageSize < 10)
				pageSize = 10;

			if (pageIndex < 0)
				pageIndex = 0;

			var totalItems = await _studentCrudService.CountAsync();
			var lastPageIndex = totalItems / pageSize;
			
			if (pageIndex > lastPageIndex)
				pageIndex = lastPageIndex;

			var items = (await _studentCrudService.GetRangeAsync(pageSize*pageIndex, pageSize))
				.Select(Extensions.ToDto)
				.ToArray();

			return new Page<StudentDto>
			{
				IsLastPage = pageIndex == lastPageIndex,
				PageIndex = pageIndex,
				Items = items,
			};
		}

		// GET: api/Test/Students
		[HttpGet("{id}")]
		public async Task<ActionResult<StudentDto>> StudentAsync(Guid? id)
		{
			if (id is null)
				return BadRequest("Corrupted Id");

			return (await _studentCrudService.GetByIdAsync(id.Value)).ToDto();
		}

		// GET: api/Test/AddStudent
		[HttpPost]
		public async Task<ActionResult> AddStudent(AddStudentRequest request)
		{
			await _studentCrudService.AddAsync(
				new Student(
					request.SexId,
					request.FirstName,
					request.MiddleName,
					request.LastName,
					request.Callsign));
			return Ok();
		}

		// GET: api/Test/Strings
		[HttpGet]
		public async Task<ActionResult<int>> StudentsCountAsync()
		{
			return await _studentCrudService.CountAsync();
		}

		/*
		// GET: api/Test/Strings
		[HttpGet]
		public async Task<ActionResult<GroupDto[]>> StudentGroups(StudentByIdRequest request)
		{
			return (await _studentRepository.GetByIdAsync(request.Id))
				.Grouping
				.Select(grouping => 
					new GroupDto
					{
						Id = grouping.Group.Id,
						Name = grouping.Group.Name
					})
				.ToArray();
		}
		*/



		/*
		// GET: api/Test/Strings
		[HttpGet]
		public async Task<ActionResult<string[]>> StringsAsync()
		{
			var student = await _studentRepository.GetAllAsync();
			var group = await _groupRepository.GetAllAsync();
			return student
				.Select(s =>
					s.GetFullname() + $" ({s.Grouping.Count}): " +
					s.Grouping
						.Select(gp => gp.Group.Name)
						.Join(", "))
				.Prepend("============== STUDENTS============== ")
				.Append("============== GROUPS ============== ")
				.Concat(group
					.Select(g =>
						g.Name + $" ({g.Grouping.Count}): " +
						g.Grouping
							.Select(gp => gp.Student.GetFullname())
							.Join(", ")))
				.ToArray();
		}  */




		/*
		[HttpPost]
		public async Task<ActionResult> AddStudentToGroup(AddStudentRequestToGroupRequest request)
		{
			var student = await _studentRepository.GetByIdAsync(request.StudentId);
			if (student is null)
				return NotFound(0);

			var group = await _groupRepository.GetByIdAsync(request.GroupId);
			if (group is null)
				return NotFound(1);

			//group.AddStudent(student);
			await _groupRepository.UpdateAsync(group);
			return Ok();
		}	
		*/

		/*
		// GET: api/Students/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Student>> GetStudent(Guid id)
		{
			var student = await _context.Student.FindAsync(id);

			if (student == null)
			{
				return NotFound();
			}

			return student;
		} */

		/*
		// PUT: api/Students/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutStudent(Guid id, Student student)
		{
			if (id != student.Id)
			{
				return BadRequest();
			}

			_context.Entry(student).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StudentExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}  
		*/

		// POST: api/Students
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		/*
		[HttpPost]
		public async Task<ActionResult<Student>> PostStudent(Student student)
		{
			_context.Student.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStudent", new { id = student.Id }, student);
		}
		*/

		/*
		[HttpPost]
		public async Task<ActionResult> PostStudent(StudentCommon student)
		{
			await Task.Delay(1000);
			
			_context.Student.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStudent", new { id = student.Id }, student);
			
			return Ok();
		}	
		*/



		/*
		// DELETE: api/Students/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Student>> DeleteStudent(Guid id)
		{
			var student = await _context.Student.FindAsync(id);
			if (student == null)
			{
				return NotFound();
			}

			_context.Student.Remove(student);
			await _context.SaveChangesAsync();

			return student;
		}

		private bool StudentExists(Guid id) => _context.Student.Any(e => e.Id == id);
		*/
	}
}
