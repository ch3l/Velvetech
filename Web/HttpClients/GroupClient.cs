using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Velvetech.Shared.Dtos;
using Velvetech.Web.HttpClients.Base;
using Velvetech.Web.HttpClients.Results;
using Velvetech.Web.HttpClients.Results.Base;

namespace Velvetech.Web.HttpClients
{
	public class GroupClient : BaseHttpClient
	{
		public GroupClient(HttpClient httpClient)
			: base(httpClient)
		{
		}

		public async Task<GroupDto[]> ListAsync(string group) => 
			await HttpClient.GetFromJsonAsync<GroupDto[]>(
				$"api/Group/List?group={group}");

		public async Task<ApiActionResult> AddAsync(GroupDto dto)
		{
			var result = await HttpClient.PostAsJsonAsync("api/Group/Add", dto);
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

		public async Task<ApiActionResult> UpdateAsync(GroupDto dto)
		{
			var result = await HttpClient.PutAsJsonAsync("api/Group/Update", dto);
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
			await HttpClient.DeleteAsync($"api/Group/Delete/{id}");

	}
}


