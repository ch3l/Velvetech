using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Velvetech.Shared;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;
using Velvetech.Web.HttpClients.Base;
using Velvetech.Web.HttpClients.Results;
using Velvetech.Web.HttpClients.Results.Base;

namespace Velvetech.Web.HttpClients
{
	public class StudentClient : BaseHttpClient
	{
		public StudentClient(HttpClient httpClient)
		:base(httpClient)
		{
		}

		public async Task<StudentDto> GetAsync(Guid id) =>
			await HttpClient.GetFromJsonAsync<StudentDto>(
				$"api/Student/Get?Id={id}");

		public async Task<Page<StudentDto>> ListAsync(StudentFilterPagedRequest request)
		{
			var result = await HttpClient.GetFromJsonAsync<Page<StudentDto>>(
				$"api/Student/List" +
				$"?pageSize={request.PageSize}" +
				$"&pageIndex={request.PageIndex}" +
				$"&sex={request.Sex?.Trim()}" +
				$"&fullname={request.Fullname?.Trim()}" +
				$"&callsign={request.Callsign?.Trim()}" +
				$"&group={request.Group?.Trim()}");

			return result;
		}

		public async Task<ClientActionResult> AddAsync(StudentDto dto)
		{
			var result = await HttpClient.PostAsJsonAsync("api/Student/Add", dto);
			
			return result.StatusCode switch
			{
				HttpStatusCode.OK => new SuccessfulEntityAction<StudentDto>(
					await result.Content.ReadFromJsonAsync<StudentDto>()),
			
				HttpStatusCode.BadRequest => new StudentErrors(
					await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>()),
				
				_ => throw new IndexOutOfRangeException(
					$"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}")
			};
		}

		public async Task<ClientActionResult> UpdateAsync(StudentDto dto)
		{
			var result = await HttpClient.PutAsJsonAsync("api/Student/Update", dto);
			
			return result.StatusCode switch
			{
				HttpStatusCode.OK => new SuccessfulEntityAction<StudentDto>(
					await result.Content.ReadFromJsonAsync<StudentDto>()),
				
				HttpStatusCode.BadRequest => new StudentErrors(
					await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>()),
				
				_ => throw new IndexOutOfRangeException(
					$"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}")
			};
		}

		public async Task DeleteAsync(Guid id) => 
			await HttpClient.DeleteAsync($"api/Student/Delete/{id}");
	}
}
