using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Services.Internal.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Presentation.Shared;
using Velvetech.Presentation.Shared.Dtos;
using Velvetech.Presentation.Shared.Requests;
using Velvetech.Web.Services.Results;

namespace Velvetech.Web.Services
{
	public class StudentService
	{
		private readonly ICrudService<Domain.Entities.Student, Guid> _studentCrudService;
		private readonly IStudentValidationService _studentValidationService;
		private readonly IListService<Sex, int> _sexList;

		public StudentService(ICrudService<Domain.Entities.Student, Guid> studentCrudService,
			IListService<Sex, int> sexList, IStudentValidationService studentValidationService)
		{
			_studentCrudService = studentCrudService;
			_sexList = sexList;
			_studentValidationService = studentValidationService;
		}

		public async Task<SexDto[]> SexListAsync() => await _sexList.ListAsync()
				.Select(Extensions.ToDto)
				.ToArrayAsync();

		public async Task<Page<StudentDto>> ListAsync(StudentFilterPagedRequest request)
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
				@group: request.Group);

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

		public async Task<StudentDto[]> ListIncludedAsync(IncludedStudentsRequest request)
		{
			if (!request.GroupId.HasValue)
				return new StudentDto[0];//BadRequest($"Corrupted or missing {nameof(request.GroupId)}");

			var filter = new IncludedStudentsSpecification(request.GroupId.Value);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(Extensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		public async Task<StudentDto[]> ListNotIncludedAsync([FromQuery] IncludedStudentsRequest request)
		{
			if (!request.GroupId.HasValue)
				return new StudentDto[0];//BadRequest($"Corrupted or missing {nameof(request.GroupId)}");

			var filter = new NotIncludedStudentsSpecification(request.GroupId.Value);
			var students = await _studentCrudService.ListAsync(filter)
				.Select(Extensions.ToDto)
				.ToArrayAsync();

			return students;
		}

		public async Task<StudentDto> GetAsync(Guid? id)
		{
			if (id is null)
				return null;// BadRequest("Corrupted Id");

			return (await _studentCrudService.GetByIdAsync(id.Value)).ToDto();
		}

		public async Task<EntityActionResult> AddAsync(StudentDto dto)
		{
			var validator = new StudentValidator(_studentValidationService);
			var entry = await Student.BuildAsync(validator, dto.SexId, dto.Firstname, dto.Middlename, dto.Lastname, dto.Callsign);

			if (entry.HasValidationErrors)
				return new StudentErrors(entry.ErrorsStrings);

			entry = await _studentCrudService.AddAsync(entry);
			return new SuccessfulEntityAction<StudentDto>(entry.ToDto());
		}

		public async Task<EntityActionResult> UpdateAsync(StudentDto dto)
		{
			var entry = await _studentCrudService.GetByIdAsync(dto.Id);
			if (entry is null)
				return new EntityNotFound();

			if (!entry.HasValidator)
			{
				var validator = new StudentValidator(_studentValidationService);
				entry.SelectValidator(validator);
			}

			entry.SetFirstname(dto.Firstname);
			entry.SetMiddlename(dto.Middlename);
			entry.SetLastname(dto.Lastname);
			await entry.SetCallsignAsync(dto.Callsign);
			entry.SetSexId(dto.SexId);

			if (entry.HasValidationErrors)
				return new StudentErrors(entry.ErrorsStrings);

			await _studentCrudService.UpdateAsync(entry);
			return new SuccessfulEntityAction<StudentDto>(entry.ToDto());
		}

		public async Task DeleteAsync(Guid id)
		{
			await _studentCrudService.DeleteAsync(id);
		}
	}
}
