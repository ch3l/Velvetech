using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Velvetech.Web
{
	public class JwtHelper
	{
		private ClaimsIdentity GetIdentity(string name, string role)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, name),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
			};

			ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			
			return claimsIdentity;
		}
	}
}
