using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Velvetech.Shared.Dtos;
using Velvetech.Web.HttpClients.Base;

namespace Velvetech.Web.HttpClients
{
	public class SexClient : BaseHttpClient
	{
		public SexClient(HttpClient httpClient)
			 : base(httpClient)
		{
		}

		public async Task<SexDto[]> ListAsync()
		{
			return await HttpClient.GetFromJsonAsync<SexDto[]>("api/Sex/List");
		}
	}
}