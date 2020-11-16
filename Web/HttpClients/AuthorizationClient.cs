using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Velvetech.Shared.Requests;
using Velvetech.Shared.Results.Authorization;
using Velvetech.Web.HttpClients.Base;
using Velvetech.Web.HttpClients.Results;

namespace Velvetech.Web.HttpClients
{
	public class AuthorizationClient : BaseHttpClient
	{
		public AuthorizationClient(HttpClient httpClient) 
			: base(httpClient)
		{
		}

		//TODO: Security lack
		public async Task<AuthorizationResult> AuthorizeAsync(AuthorizationRequest request)
		{
			var result = await HttpClient.PostAsJsonAsync("api/Authorization/Authorize", request);

			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
				{
					var token = await result.Content.ReadAsStringAsync();
					return new AuthorizationSuccess(token);
				}
				
				case HttpStatusCode.BadRequest:
					return new AuthorizationFail();
				
				default:
					throw new IndexOutOfRangeException(nameof(result.StatusCode));
			}
		}
	}
}