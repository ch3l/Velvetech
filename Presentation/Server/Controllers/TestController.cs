using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Velvetech.Domain.Common;
using Velvetech.Presentation.Server.Models;


namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IAsyncRepository<Domain.Entities.StudentAggregate.Student, Guid> _studentRepository;
		private readonly IAsyncRepository<Domain.Entities.GroupAggregate.Group, Guid> _groupRepository;

		public TestController(	IAsyncRepository<Domain.Entities.GroupAggregate.Group, Guid> groupRepository, IAsyncRepository<Domain.Entities.StudentAggregate.Student, Guid> studentRepository)
		{
			_groupRepository = groupRepository;
			_studentRepository = studentRepository;
		}

		// GET: api/Students
		[HttpGet]
		public async Task<ActionResult<string[]>> StringsAsync()
		{
			var x = await _studentRepository.ListAllAsync();
			//var x = await _groupRepository.ListAllAsync();
			return x.Select(x => x.Sex?.Name ?? "<NULL>").ToArray();

			//return Enumerable.Range(0,10).Select(x=>x.ToString()).ToArray();
		}

		/*
		// GET: api/Students
		public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
		{
			
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
