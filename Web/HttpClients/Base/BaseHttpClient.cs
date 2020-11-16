using System.Net.Http;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Velvetech.Web.HttpClients.Base
{
	public abstract class BaseHttpClient
	{
		protected readonly HttpClient HttpClient;

		protected BaseHttpClient(HttpClient httpClient)
		{
			HttpClient = httpClient;
		}

		public void UseToken(string token) => HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
	}
}