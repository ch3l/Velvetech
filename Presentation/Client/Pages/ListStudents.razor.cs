using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using Presentation.Shared.Dtos;

namespace Presentation.Client.Pages
{
	public partial class ListStudents : ComponentBase
	{
		int counter, studentCounter;
		IEnumerable<StudentDto> students;
		string Title = string.Empty;

		async Task AddStudentAsync()
		{

		}

		async Task EditAsync(Guid guid)
		{
			await Task.FromResult(Title = "Edit: " + guid.ToString());
		}

		async Task RemoveAsync(Guid guid)
		{
			await Task.FromResult(Title = "Remove: " + guid.ToString());
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
			students = await Http.GetFromJsonAsync<IEnumerable<StudentDto>>($"api/Students/Students");
		}

		protected override async Task OnInitializedAsync()
		{
			await LoadRecordsAsync();
		}
	}
}
