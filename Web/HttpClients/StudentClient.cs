using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Velvetech.Shared;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;
using Velvetech.Web.HttpClients.Results;

namespace Velvetech.Web.HttpClients
{
	public class StudentClient
	{
		private readonly HttpClient _httpClient;

		public StudentClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<StudentDto> GetAsync(Guid id) =>
			await _httpClient.GetFromJsonAsync<StudentDto>(
				$"api/Student/Get?Id={id}");

		public async Task<Page<StudentDto>> ListAsync(StudentFilterPagedRequest request) => 
			await _httpClient.GetFromJsonAsync<Page<StudentDto>>(
				$"api/Student/List" +
				$"?pageSize={request.PageSize}" +
				$"&pageIndex={request.PageIndex}" +
				$"&sex={request.Sex}" +
				$"&fullname={request.Fullname}" +
				$"&callsign={request.Callsign}" +
				$"&group={request.Group}");

		public async Task<EntityActionResult> AddAsync(StudentDto dto)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Student/Add", dto);
			
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

		public async Task<EntityActionResult> UpdateAsync(StudentDto dto)
		{
			var result = await _httpClient.PutAsJsonAsync("api/Student/Update", dto);
			
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
			await _httpClient.DeleteAsync($"api/Student/Delete/{id}");
	}
}
