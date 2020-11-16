using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;
using Velvetech.Web.HttpClients.Results;

namespace Velvetech.Web.HttpClients
{
	public class GroupClient
	{
		private readonly HttpClient _httpClient;

		public GroupClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<GroupDto[]> ListAsync(string group) => 
			await _httpClient.GetFromJsonAsync<GroupDto[]>(
				$"api/Group/List?group={group}");

		public async Task<EntityActionResult> AddAsync(GroupDto dto)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Group/Add", dto);
			return result.StatusCode switch
			{
				HttpStatusCode.OK => new SuccessfulEntityAction<GroupDto>(
					await result.Content.ReadFromJsonAsync<GroupDto>()),
			
				HttpStatusCode.BadRequest => new GroupErrors(
					await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>()),
				
				_ => throw new IndexOutOfRangeException(
					$"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}")
			};
		}

		public async Task<EntityActionResult> UpdateAsync(GroupDto dto)
		{
			var result = await _httpClient.PutAsJsonAsync("api/Group/Update", dto);
			return result.StatusCode switch
			{
				HttpStatusCode.OK => new SuccessfulEntityAction<GroupDto>(
					await result.Content.ReadFromJsonAsync<GroupDto>()),
			
				HttpStatusCode.BadRequest => new GroupErrors(
					await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>()),
				
				_ => throw new IndexOutOfRangeException(
					$"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}")
			};
		}

		public async Task DeleteAsync(Guid id) => 
			await _httpClient.DeleteAsync($"api/Group/Delete/{id}");

	}
}


