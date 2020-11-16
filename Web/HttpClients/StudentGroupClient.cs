using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Velvetech.Shared.Dtos;
using Velvetech.Shared.Requests;

namespace Velvetech.Web.HttpClients
{
	public class StudentGroupClient
	{
		private readonly HttpClient _httpClient;

		public StudentGroupClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<StudentDto[]> ListIncludedAsync(IncludedStudentsRequest request) =>
			await _httpClient.GetFromJsonAsync<StudentDto[]>(
				$"api/StudentGroup/ListIncluded?GroupId={request.GroupId}");

		public async Task<StudentDto[]> ListNotIncludedAsync(IncludedStudentsRequest request) =>
			await _httpClient.GetFromJsonAsync<StudentDto[]>(
				$"api/StudentGroup/ListNotIncluded?GroupId={request.GroupId}");

		public async Task<bool> IncludeStudentAsync(StudentGroupRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("api/StudentGroup/IncludeStudent", request);
			return result.StatusCode switch
			{
				HttpStatusCode.OK => true,

				HttpStatusCode.NotFound => false,

				_ => throw new IndexOutOfRangeException(
					$"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(IncludeStudentAsync)}")
			};
		}

		public async Task<bool> ExcludeStudentAsync(StudentGroupRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("api/StudentGroup/ExcludeStudent", request);
			return result.StatusCode switch
			{
				HttpStatusCode.OK => true,

				HttpStatusCode.NotFound => false,

				_ => throw new IndexOutOfRangeException(
					$"{nameof(result.StatusCode)} in {GetType().Name}.{nameof(IncludeStudentAsync)}")
			};
		}
	}
}