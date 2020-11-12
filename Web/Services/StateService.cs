using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Velvetech.Web.Services
{
	public class StateService
	{
		private readonly HttpClient _httpClient;

		public StateService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> IsApiReady() =>
			await _httpClient.GetFromJsonAsync<bool>("api/State/IsReady");
	}
}
