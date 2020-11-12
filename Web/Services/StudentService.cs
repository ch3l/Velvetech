using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Velvetech.Shared;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;
using Velvetech.Web.Services.Results;

namespace Velvetech.Web.Services
{
	public class StudentService
	{
		private readonly HttpClient _httpClient;

		public StudentService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<SexDto[]> SexListAsync() => 
			await _httpClient.GetFromJsonAsync<SexDto[]>("api/Students/SexList");

		public async Task<Page<StudentDto>> ListAsync(StudentFilterPagedRequest request) => 
			await _httpClient.GetFromJsonAsync<Page<StudentDto>>(
				$"api/Students/List" +
				$"?pageSize={request.PageSize}" +
				$"&pageIndex={request.PageIndex}" +
				$"&sex={request.Sex}" +
				$"&fullname={request.Fullname}" +
				$"&callsign={request.Callsign}" +
				$"&group={request.Group}");

		public async Task<StudentDto[]> ListIncludedAsync(IncludedStudentsRequest request) => 
			await _httpClient.GetFromJsonAsync<StudentDto[]>(
				$"api/Students/ListIncluded?GroupId={request.GroupId}");

		public async Task<StudentDto[]> ListNotIncludedAsync(IncludedStudentsRequest request) => 
			await _httpClient.GetFromJsonAsync<StudentDto[]>(
				$"api/Students/ListNotIncluded?GroupId={request.GroupId}");

		public async Task<StudentDto> GetAsync(Guid id) => 
			await _httpClient.GetFromJsonAsync<StudentDto>(
				$"api/Students/Get?Id={id}");

		public async Task<EntityActionResult> AddAsync(StudentDto dto)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Students/Add", dto);
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return new SuccessfulEntityAction<StudentDto>(await result.Content.ReadFromJsonAsync<StudentDto>());

				case HttpStatusCode.BadRequest:
					return new StudentErrors(await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>());

				default:
					throw new IndexOutOfRangeException($"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}");
			}
		}

		public async Task<EntityActionResult> UpdateAsync(StudentDto dto)
		{
			var result = await _httpClient.PutAsJsonAsync("api/Students/Update", dto);
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return new SuccessfulEntityAction<StudentDto>(await result.Content.ReadFromJsonAsync<StudentDto>());

				case HttpStatusCode.BadRequest:
					return new StudentErrors(await result.Content.ReadFromJsonAsync<Dictionary<string, string[]>>());

				default:
					throw new IndexOutOfRangeException($"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(AddAsync)}");
			}
		}

		public async Task DeleteAsync(Guid id) => 
			await _httpClient.DeleteAsync($"api/Students/Delete/{id}");
	}
}
