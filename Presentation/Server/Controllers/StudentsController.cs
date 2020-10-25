using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;
using Velvetech.Presentation.Shared;
using Velvetech.Presentation.Shared.Dtos;

namespace Velvetech.Presentation.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		readonly ICrudService<Student, Guid> _studentCrudService;
		readonly IListService<Sex, int> _sexList;

		public StudentsController(ICrudService<Student, Guid> studentCrudService,
			IListService<Sex, int> sexList)
		{
			_studentCrudService = studentCrudService;
			_sexList = sexList;
		}

		IQueryable<Student> FilterFunc(
			IQueryable<Student> source,
			string sex,
			string fullname,
			string callsign,
			string group)
		{
			Expression<Func<Student, bool>> ignoreExpression = (student) => true;
			
			Expression<Func<Student, bool>> sexFilter = 
				sex is null 
					? ignoreExpression
					:(student) => student.Sex.Name.Equals(sex);

			Expression<Func<Student, bool>> fullNameFilter = 
				fullname is null
					? ignoreExpression
					: (student) => (student.Firstname + " " + student.Middlename + " " + student.Lastname).Contains(fullname);

			Expression<Func<Student, bool>> callsignFilter =
				callsign is null
					? ignoreExpression
					: (student) => student.Callsign.Contains(callsign);

			Expression<Func<Student, bool>> groupFilter =
				group is null
					? ignoreExpression
					: (student) => student.Grouping.Any(grouping => grouping.Group.Name.Contains(group));

			return source
				.Where(sexFilter
					.And(fullNameFilter)
					.And(callsignFilter)
					.And(groupFilter));
		}

		// GET: api/Test/Students
		[HttpGet]
		public async Task<ActionResult<Page<StudentDto>>> ListAsync(
			string sex,
			string fullname,
			string callsign,
			string group,
			int pageSize = 10,
			int pageIndex = 0)
		{
			if (pageSize < 10)
				pageSize = 10;

			var filter = new FilterBase<Student>(x => FilterFunc(x, sex, fullname, callsign, group));
			var totalItems = await _studentCrudService.CountAsync(filter);
			var lastPageIndex = totalItems / pageSize;

			if (totalItems == pageSize * lastPageIndex)
				lastPageIndex--;

			if (pageIndex > lastPageIndex)
				pageIndex = lastPageIndex;

			if (pageIndex < 0)
				pageIndex = 0;

			var items = await _studentCrudService.GetRangeAsync(pageSize * pageIndex, pageSize, filter)
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

		// GET: api/Test/Get
		[HttpGet("{id}")]
		public async Task<ActionResult<StudentDto>> GetAsync(Guid? id)
		{
			if (id is null)
				return BadRequest("Corrupted Id");

			return (await _studentCrudService.GetByIdAsync(id.Value)).ToDto();
		}

		// GET: api/Test/StudentsCount
		[HttpGet]
		public async Task<ActionResult<int>> StudentsCountAsync()
		{
			return await _studentCrudService.CountAsync();
		}

		// PUT: api/Students
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<IActionResult> AddAsync(StudentDto dto)
		{
			var item = dto.FromDto();

			try
			{
				await _studentCrudService.AddAsync(item);
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return Ok("Updated successfully");
		}


		// PUT: api/Students
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut]
		public async Task<IActionResult> UpdateAsync(StudentDto dto)
		{
			var item = dto.FromDto();

			try
			{
				await _studentCrudService.UpdateAsync(item);
			}
			catch (Exception)
			{
				if ((await _studentCrudService.GetByIdAsync(item.Id)) is null)
					return NotFound();
				else
					throw;
			}

			return Ok("Updated successfully");
		}



		// DELETE: api/Students/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAsync(Guid id)
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
	}
}
