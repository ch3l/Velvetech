using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Shared;
using Velvetech.Shared.Results.Authorization;

namespace Velvetech.Api.Services
{
	public class AuthorizationService 
	{
		private readonly IUsersRolesService _usersRolesService;
		private readonly ILogger<AuthorizationService> _logger;

		public AuthorizationService(IUsersRolesService usersRolesService, 
			ILogger<AuthorizationService> logger)
		{
			_usersRolesService = usersRolesService;
			_logger = logger;
		}

		public async Task<AuthorizationResult> Authorize(string userName, string password)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var user = await _usersRolesService.GetUser(userName);
			if (user is null)
				return new AuthorizationFail();

			if (user.Password != password)
				return new AuthorizationFail();

			var roles = user.GetRoles();
			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id) };

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role.Id));
			}

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Audience = JwtShared.Audience,
				Issuer = JwtShared.Issuer,
				Subject = new ClaimsIdentity(claims.ToArray()),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(JwtShared.SecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			SecurityToken token;
			try
			{
				token = tokenHandler.CreateToken(tokenDescriptor);
			}
			catch (Exception e)
			{
				_logger.LogCritical(e.ToString());
				throw;
			}

			string tokenString;
			try
			{
				tokenString = tokenHandler.WriteToken(token);
			}
			catch (Exception e)
			{
				_logger.LogCritical(e.ToString());
				throw;
			}	

			var result = new AuthorizationSuccess(tokenString);

			return result;
		}
	}
}