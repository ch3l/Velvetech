using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Velvetech.Web.HttpClients
{
	public class StateClient
	{
		private readonly HttpClient _httpClient;

		public StateClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> IsApiReady() =>
			await _httpClient.GetFromJsonAsync<bool>("api/State/IsReady");
	}
}
