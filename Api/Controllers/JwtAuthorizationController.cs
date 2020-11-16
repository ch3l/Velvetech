using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Specifications;
using Velvetech.Shared.Dtos;

namespace Velvetech.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class JwtAuthorizationController : ControllerBase
	{
		private readonly IReadService<User, string> _userReadService;

		public JwtAuthorizationController(IReadService<User, string> userReadService)
		{
			_userReadService = userReadService;
		}

		// GET: api/Group/List
		[HttpGet]
		public async Task<string> AuthorizeAsync(string userId)
		{
			var user = await _userReadService.GetByIdAsync(userId);
			if (user is null)
				return null;

			throw new NotImplementedException();

			/*
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

			return response;  */
		}
	}
}