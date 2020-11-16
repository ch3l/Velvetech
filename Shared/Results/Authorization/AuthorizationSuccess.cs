namespace Velvetech.Shared.Results.Authorization
{
	public class AuthorizationSuccess : AuthorizationResult
	{
		public string Token { get; }

		public AuthorizationSuccess(string token)
		{
			Token = token;
		}
	}
}