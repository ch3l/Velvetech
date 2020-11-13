using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Entities.Validators;
using Velvetech.Domain.Services.External.Interfaces;

namespace Velvetech.Domain.Services.External
{
	class UsersRolesService : IUsersRolesService
	{
		private readonly IAsyncRepository<User, Guid> _userRepository;
		private readonly IAsyncRepository<Role, Guid> _roleRepository;

		public UsersRolesService(
			IAsyncRepository<User, Guid> userRepository, 
			IAsyncRepository<Role, Guid> roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}

		public async Task<bool> AddUserAsync(string userName)
		{
			var validator = new UserValidator();
			var user = User.Build(validator, userName);

			if (user.HasErrors)
				return false;

			await _userRepository.AddAsync(user);

			return true;
		}

		public async Task<bool> AddRoleAsync(string roleName)
		{
			var validator = new RoleValidator();
			var role = Role.Build(validator, roleName);

			if (role.HasErrors)
				return false;

			await _roleRepository.AddAsync(role);
			return true;
		}

		public Task<bool> AddRoleToUserAsync(string userName, string roleName)
		{
			throw new NotImplementedException();
		}

		public Task<string[]> GetUserRolesAsync(string userName)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveRoleFromUserAsync(string userName, string roleName)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAllRolesFromUserAsync(string userName, string roleName)
		{
			throw new NotImplementedException();
		}
	}
}
