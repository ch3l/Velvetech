using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Velvetech.Api.Services;
using Velvetech.Shared.Requests;
using Velvetech.Shared.Results.Authorization;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{
		private readonly AuthorizationService _authorizationService;

		public AuthorizationController(AuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}

		// Post: api/Authorization/Authorize
		[HttpPost]
		[SwaggerOperation(
			Summary = "Authorizes a user",
			Description = @"Authorizes a user<br/>Login: <b>User</b><br/>Password: <b>Pewpew</b>",
			OperationId = "AuthorizationController.AuthorizeAsync",
			Tags = new[]
			{
				"Controller: Authorization"
			})]
		public async Task<ActionResult<string>> AuthorizeAsync(AuthorizationRequest request)
		{
			var authorizationResult = await _authorizationService.Authorize(request.User, request.Password);
			
			return authorizationResult switch
			{
				AuthorizationFail _ => 
					BadRequest("Bad credentials"),
			
				AuthorizationSuccess authorizationSuccess 
					=> authorizationSuccess.Token,
				
				_ => throw new NotImplementedException(nameof(AuthorizationResult))
			};
		}
	}
}