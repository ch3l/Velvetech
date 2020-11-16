using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Shared.Authentication
{
	public static class Constants
	{
		public static class Roles
		{
			public const string SexRead = nameof(SexRead);
			public const string StudentRead = nameof(StudentRead);
			public const string StudentCrud = nameof(StudentCrud);
			public const string GroupRead = nameof(GroupRead);
			public const string GroupCrud = nameof(GroupCrud);
			public const string StudentGroupRead = nameof(StudentGroupRead);
			public const string StudentGroupCrud = nameof(StudentGroupCrud);
		}

		public static class Users
		{
			public static class User
			{
				public const string Name = nameof(User);
				public const string Password = "Pewpew";
			}
		}
	}
}
