using System.Net.Http;

namespace Velvetech.Web.HttpClients.Base
{
	public abstract class BaseHttpClient
	{
		protected readonly HttpClient HttpClient;

		protected BaseHttpClient(HttpClient httpClient)
		{
			HttpClient = httpClient;
		}
	}
}