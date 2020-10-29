﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Presentation.Shared;
using Velvetech.Presentation.Shared.Dtos;
using Velvetech.Presentation.Shared.Requests;

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

		// GET: api/Test/Students
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

			if (totalItems == pageSize * lastPageIndex)
				lastPageIndex--;

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
				group: request.Group);

			var students = await _studentCrudService.ListAsync(filter)
				.Select(Extensions.ToDto)
				.ToArrayAsync();

			return new Page<StudentDto>
			{
				IsLastPage = pageIndex == lastPageIndex,
				PageIndex = pageIndex,
				PageSize = pageSize,
				Items = students,
			};
		}

		// GET: api/Test/ListIncluded
		[HttpGet]
		public async Task<ActionResult<StudentDto[]>> ListIncludedAsync([FromQuery] IncludedStudentsRequest request)
		{
			if (!request.GroupId.HasValue)
				return BadRequest($"Corrupted or missing {nameof(request.GroupId)}");

			var filter = new IncludedStudentsSpecification(request.GroupId.Value);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(Extensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		// GET: api/Test/ListIncluded
		[HttpGet]
		public async Task<ActionResult<StudentDto[]>> ListNotIncludedAsync([FromQuery] IncludedStudentsRequest request)
		{
			if (!request.GroupId.HasValue)
				return BadRequest($"Corrupted or missing {nameof(request.GroupId)}");

			var filter = new NotIncludedStudentsSpecification(request.GroupId.Value);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(Extensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		// GET: api/Test/Students
		[HttpGet]
		public async Task<ActionResult<SexDto[]>> SexListAsync()
		{
			return await _sexList.ListAsync()
				.Select(Extensions.ToDto)
				.ToArrayAsync();
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
				if (await _studentCrudService.GetByIdAsync(item.Id) is null)
					return NotFound();
				
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
		}
	}
}
