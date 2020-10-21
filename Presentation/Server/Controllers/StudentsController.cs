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
using Velvetech.Domain.Entities;
using Domain.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Shared;
using Microsoft.EntityFrameworkCore;

namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{				
		ICrudService<Student, Guid> _studentCrudService;
		IGroupingService _groupingService;
		IListService<Sex> _sexList;

		public StudentsController(ICrudService<Student, Guid> studentCrudService, 
			IGroupingService groupingService,
			IListService<Sex> sexList)
		{
			_studentCrudService = studentCrudService;
			_groupingService = groupingService;
			_sexList = sexList;
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

			if (totalItems == pageSize * lastPageIndex)
				lastPageIndex--;

			if (pageIndex > lastPageIndex)
				pageIndex = lastPageIndex;

			var items = await _studentCrudService.GetRangeAsync(pageSize * pageIndex, pageSize)
				.Select(Extensions.ToDto)
				.ToArrayAsync();

			return new Page<StudentDto>
			{
				IsLastPage = pageIndex == lastPageIndex,
				PageIndex = pageIndex,
				PageSize = pageSize,
				Items = items,
			};
		}

		// GET: api/Test/Students
		[HttpGet]
		public async Task<ActionResult<SexDto[]>> SexListAsync()
		{
			return await (_sexList.GetAllAsync()
				.Select(Extensions.ToDto)
				.ToArrayAsync());
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

		
		// PUT: api/Students
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut]
		public async Task<IActionResult> Update(StudentDto dto)
		{
			var student = dto.FromDto();			

			try
			{
				await _studentCrudService.UpdateAsync(student);
			}
			catch (Exception)
			{
				if ((await _studentCrudService.GetByIdAsync(student.Id)) is null)
					return NotFound();
				else
					throw;
			}	

			return Ok("Updated successfully");
		}

		// PUT: api/Students
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<IActionResult> Add(StudentDto dto)
		{
			var student = dto.FromDto();

			try
			{
				await _studentCrudService.AddAsync(student);
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return Ok("Updated successfully");
		}
		
		// DELETE: api/Students/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(Guid id)
		{
			await _studentCrudService.DeleteAsync(id);
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
