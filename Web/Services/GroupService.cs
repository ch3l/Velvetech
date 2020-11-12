using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Velvetech.Web.Services.Results;

using GroupDto = Velvetech.Shared.Dtos.GroupDto;
using StudentGroupRequest = Velvetech.Shared.Requests.StudentGroupRequest;

namespace Velvetech.Web.Services
{
	public class GroupService
	{
		private readonly HttpClient _httpClient;

		public GroupService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<GroupDto[]> ListAsync(string group) => 
			await _httpClient.GetFromJsonAsync<GroupDto[]>(
				$"api/Groups/List?group={group}");

		public async Task<EntityActionResult> AddAsync(GroupDto dto)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Groups/Add", dto);
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
			var result = await _httpClient.PutAsJsonAsync("api/Groups/Update", dto);
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

		public async Task DeleteAsync(Guid id) => 
			await _httpClient.DeleteAsync($"api/Groups/Delete/{id}");

		public async Task<bool> IncludeStudentAsync(StudentGroupRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Groups/IncludeStudent", request);
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
			var result = await _httpClient.PostAsJsonAsync("api/Groups/ExcludeStudent", request);
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


