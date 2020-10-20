using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using Presentation.Shared.Dtos;

namespace Presentation.Client.Pages
{
	public partial class ListStudents : ComponentBase
	{
		int counter, studentCounter;
		StudentDto[] students;
		string title = string.Empty;

		async Task AddStudentAsync()
		{

		}

		async Task EditAsync(int index)
		{
			var studentId = students[index].Id;
			var student = await Http.GetFromJsonAsync<StudentDto>($"api/Students/Student/{studentId}");
			title = student.Callsign;
		}

		async Task RemoveAsync(int index)
		{
			await Task.FromResult(title = "Remove: " + students[index].Id.ToString());
		}

		async Task GenerateStudentAsync()
		{
			studentCounter = await Http.GetFromJsonAsync<int>("api/Students/StudentsCount");
			studentCounter++;

			await Http.PostAsJsonAsync("api/Students/AddStudent",
				new Presentation.Shared.Requests.AddStudentRequest(
					1,
					"First name " + studentCounter,
					"Middle name " + studentCounter,
					"Last name " + studentCounter,
					"Callsign " + studentCounter));

			await LoadRecordsAsync();
		}

		async Task LoadRecordsAsync()
		{
			students = (await Http.GetFromJsonAsync<IEnumerable<StudentDto>>($"api/Students/Students")).ToArray();
		}

		protected override async Task OnInitializedAsync()
		{
			await LoadRecordsAsync();
		}
	}
}
