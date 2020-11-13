using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Services.External.Interfaces
{
	public interface IUsersRolesService
	{
		Task<bool> AddUserAsync(string userName);
		Task<bool> AddRoleAsync(string roleName);
		Task<bool > AddRoleToUserAsync(string userName, string roleName);
		Task<string[]> GetUserRolesAsync(string userName);

		Task<bool> RemoveRoleFromUserAsync(string userName, string roleName);
		Task<bool> RemoveAllRolesFromUserAsync(string userName, string roleName);
	}
}
