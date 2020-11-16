using System.Net.Http;
using Velvetech.Web.HttpClients.Base;

namespace Velvetech.Web.HttpClients
{
	public class JwtAuthorizationClient : BaseHttpClient
	{
		public JwtAuthorizationClient(HttpClient httpClient) 
			: base(httpClient)
		{
		}
	}
}