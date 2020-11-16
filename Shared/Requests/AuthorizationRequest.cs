namespace Velvetech.Shared.Requests
{
	public class AuthorizationRequest
	{
		public string User { get; set; }
		public string Password { get; set; }

		public AuthorizationRequest(string user, string password)
		{
			User = user;
			Password = password;
		}

		public AuthorizationRequest()
		{
		}
	}
}