using System.Linq;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Domain.Specifications;

namespace Velvetech.Domain.Services.External.Particular
{
	public class UsersRolesService : IUsersRolesService
	{
		private readonly IAsyncRepository<User, string> _userRepository;
		private readonly IAsyncRepository<Role, string> _roleRepository;

		public UsersRolesService(
			IAsyncRepository<User, string> userRepository, 
			IAsyncRepository<Role, string> roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}

		public async Task AddUserAsync(string userName, string password)
		{
			var user = new User(userName, password);
			await _userRepository.AddAsync(user);
		}

		public async Task AddRoleAsync(string roleName)
		{
			var role = new Role(roleName);
			await _roleRepository.AddAsync(role);
		}

		public async Task AddRoleToUserAsync(string userName, string roleName)
		{
			var user = await _userRepository.FirstOrDefault(userName, new UserSpecification());
			var role = await _roleRepository.GetById(roleName);

			user.AddRole(role);
			await _userRepository.UpdateAsync(user);
		}

		public async Task<User> GetUser(string userName) => await _userRepository.FirstOrDefault(userName, new UserSpecification());

		public async Task<string[]> GetUserRolesAsync(string userName)
		{
			var user = await _userRepository.FirstOrDefault(userName, new UserSpecification());
			return user.GetRoles().Select(role => role.Id).ToArray();
		}
	}
}
