﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Velvetech.Shared
{
	public static class JwtShared
	{
		public const string Issuer = "Issuer";

		public const string Audience = "Audience";

		public const string Key = "BlablaKey";
		
		public const string Password = "Pass@word1";

		public static SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
	}
}
