using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Velvetech.Shared.Dtos;

namespace Velvetech.Web.HttpClients
{
	public class SexClient
	{
		private readonly HttpClient _httpClient;

		public SexClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<SexDto[]> ListAsync() =>
			await _httpClient.GetFromJsonAsync<SexDto[]>("api/Sex/List");
	}
}