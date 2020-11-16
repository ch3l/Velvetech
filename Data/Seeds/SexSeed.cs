using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Velvetech.Data.Seeds.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Specifications;

namespace Velvetech.Data.Seeds
{
	public class SexSeed : Seed<Sex, int>
	{
		public SexSeed(ICrudService<Sex, int> crudService)
			: base(crudService)
		{
		}

		protected override async Task AddEntities()
		{
			await CrudService.AddAsync(new Sex(1, "Female"));
			await CrudService.AddAsync(new Sex(2, "Male"));
		}
	}

	public class UserSeed : Seed<User, string>
	{
		private readonly IReadService<Role, string> _roleReadService;

		public UserSeed(
			ICrudService<User, string> crudService,
			IReadService<Role, string> roleReadService)
			: base(crudService)
		{
			_roleReadService = roleReadService;
		}

		protected override async Task AddEntities()
		{
			var user = new User("User", "Pewpew");

			var studentUserRole = await _roleReadService.GetByIdAsync("StudentUser");
			var studentAdminRole = await _roleReadService.GetByIdAsync("StudentAdmin");
			var groupUserRole = await _roleReadService.GetByIdAsync("GroupUser");
			var groupAdminRole = await _roleReadService.GetByIdAsync("GroupAdmin");

			user.AddRole(studentUserRole);
			user.AddRole(studentAdminRole);
			user.AddRole(groupUserRole);
			user.AddRole(groupAdminRole);

			await CrudService.AddAsync(user);
		}
	}

	public class RoleSeed : Seed<Role, string>
	{
		public RoleSeed(ICrudService<Role, string> crudService)
			: base(crudService)
		{
		}

		protected override async Task AddEntities()
		{
			await CrudService.AddAsync(new Role("StudentUser"));
			await CrudService.AddAsync(new Role("StudentAdmin"));
			await CrudService.AddAsync(new Role("GroupUser"));
			await CrudService.AddAsync(new Role("GroupAdmin"));
		}
	}
}
