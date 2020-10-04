using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Velvetech.Server.Models;
using Velvetech.Shared;
using Velvetech.Shared.Wrappers;

namespace Velvetech.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly VelvetechContext _context;

		public StudentsController(VelvetechContext context)
		{
			_context = context;
		}

		// GET: api/Students
		[HttpGet]
		public async Task<ActionResult<IEnumerable<StudentWrapper>>> GetStudent(
			string sex = null,
			string fullName = null,
			string callsign = null,
			string group = null)
		{
			return await _context.Student
				.Select(student =>
					new StudentWrapper
					{
						Id = student.Id,
						FullName = student.LastName + " " + student.Name + " " + student.Patronymic,
						Callsign = student.Callsign,
						Sex = student.Sex.Name,
						Groups = from grouping in student.Grouping select grouping.Group.Name
					})
				.Where(wrapper =>
					(sex == null || wrapper.Sex == sex) &&
					(fullName == null || wrapper.FullName.Contains(fullName)) &&
					(callsign == null || wrapper.Callsign.Contains(callsign)) &&
					(group == null || wrapper.Groups.Any(gr => gr.Contains(group))))
				.ToListAsync();
		}

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
		}

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

		// POST: api/Students
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Student>> PostStudent(Student student)
		{
			_context.Student.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStudent", new { id = student.Id }, student);
		}

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
	}
}
