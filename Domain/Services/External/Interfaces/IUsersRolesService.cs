using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Services.External.Interfaces
{
	public interface IUsersRolesService
	{
		Task AddUserAsync(string userName);
		Task AddRoleAsync(string roleName);
		Task AddRoleToUserAsync(string userName, string roleName);
		Task<User> GetUser(string userName);
	}
}
