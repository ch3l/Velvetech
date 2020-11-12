using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Velvetech.Shared;

namespace Velvetech.Api.Services
{
	public interface ITokenClaimsService
	{
		//Task<string> GetTokenAsync(string userName);
	}

	public class IdentityTokenClaimService : ITokenClaimsService
	{
		public IdentityTokenClaimService()
		{
		}

		/*
		public async Task<string> GetTokenAsync(string userName)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = JwtShared.SecurityKey;
			var user = await _userManager.FindByNameAsync(userName);
			var roles = await _userManager.GetRolesAsync(user);
			var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims.ToArray()),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}*/
	}
}
