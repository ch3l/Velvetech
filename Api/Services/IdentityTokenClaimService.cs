using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Shared;

namespace Velvetech.Api.Services
{
	public interface ITokenClaimsService
	{
		Task<string> GetTokenAsync(string userName);
	}

	public class IdentityTokenClaimService : ITokenClaimsService
	{
		private readonly IUsersRolesService _usersRolesService;

		public IdentityTokenClaimService(IUsersRolesService usersRolesService)
		{
			_usersRolesService = usersRolesService;
		}

		public async Task<string> GetTokenAsync(string userName)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var securityKey = JwtShared.SecurityKey;

			var user = await _usersRolesService.GetUser(userName);
			var roles = user.GetRoles();
			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id) };

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role.Id));
			}

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims.ToArray()),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}