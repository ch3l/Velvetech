using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Velvetech.Api.Services;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	public class Authenticate  
	{
		private readonly ITokenClaimsService _tokenClaimsService;

		public Authenticate(ITokenClaimsService tokenClaimsService)
		{
			_tokenClaimsService = tokenClaimsService;
		}

		/*
		[HttpPost("api/authenticate")]
		public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken)
		{
			var response = new AuthenticateResponse(request.CorrelationId());

			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, set lockoutOnFailure: true
			//var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
			var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

			response.Result = result.Succeeded;
			response.IsLockedOut = result.IsLockedOut;
			response.IsNotAllowed = result.IsNotAllowed;
			response.RequiresTwoFactor = result.RequiresTwoFactor;
			response.Username = request.Username;
			response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);

			return response;
		}	 
		*/
	}
}
