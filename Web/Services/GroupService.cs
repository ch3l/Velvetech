using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validations;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared;
using Velvetech.Shared.Dtos;
using Velvetech.Web.Services.Results;
using GroupDto = Velvetech.Shared.Dtos.GroupDto;
using StudentGroupRequest = Velvetech.Shared.Requests.StudentGroupRequest;

namespace Velvetech.Web.Services
{
	public class GroupService
	{
		private readonly ICrudService<Group, Guid> _groupCrudService;
		private readonly IGroupingService _groupingService;

		private readonly HttpClient HttpClient;

		public GroupService(ICrudService<Group, Guid> groupCrudService, IGroupingService groupingService)
		{
			_groupCrudService = groupCrudService;
			_groupingService = groupingService;

			HttpClient = new HttpClient
			{
				BaseAddress = new Uri("http://localhost:5000")
			};
		}

		public async Task<GroupDto[]> ListAsync(string group)
		{
			return await HttpClient.GetFromJsonAsync<GroupDto[]>(
				$"api/Groups/List?group={group}");
		}

		public async Task<EntityActionResult> AddAsync(GroupDto dto)
		{
			var result = await HttpClient.PostAsJsonAsync("api/Groups/Add", dto);
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return new SuccessfulEntityAction<GroupDto>(await result.Content.ReadFromJsonAsync<GroupDto>());

				case HttpStatusCode.BadRequest:
					return new GroupErrors(await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>());

				default:
					throw new IndexOutOfRangeException($"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}");
			}
		}

		public async Task<EntityActionResult> UpdateAsync(GroupDto dto)
		{
			var result = await HttpClient.PutAsJsonAsync("api/Groups/Update", dto);
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return new SuccessfulEntityAction<GroupDto>(await result.Content.ReadFromJsonAsync<GroupDto>());

				case HttpStatusCode.BadRequest:
					return new GroupErrors(await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>());

				default:
					throw new IndexOutOfRangeException($"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}");
			}
		}
		public async Task DeleteAsync(Guid id)
		{
			await HttpClient.DeleteAsync($"api/Groups/Delete/{id}");
		}

		public async Task<bool> IncludeStudentAsync(StudentGroupRequest request)
		{
			var result = await HttpClient.PostAsJsonAsync("api/Groups/IncludeStudent", request);
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return true;

				case HttpStatusCode.NotFound:
					return false;
				
				default:
					throw new IndexOutOfRangeException($"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(IncludeStudentAsync)}");
			}
		}

		public async Task<bool> ExcludeStudentAsync(StudentGroupRequest request)
		{
			var result = await HttpClient.PostAsJsonAsync("api/Groups/ExcludeStudent", request);
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return true;

				case HttpStatusCode.NotFound:
					return false;

				default:
					throw new IndexOutOfRangeException($"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(IncludeStudentAsync)}");
			}
		}
	}
}


