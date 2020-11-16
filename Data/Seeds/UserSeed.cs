using System.Threading.Tasks;

using Velvetech.Data.Seeds.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;

namespace Velvetech.Data.Seeds
{
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

			var sexReadRole = await _roleReadService.GetByIdAsync("SexRead");
			var studentReadRole = await _roleReadService.GetByIdAsync("StudentRead");
			var studentCrudRole = await _roleReadService.GetByIdAsync("StudentCrud");
			var groupReadRole = await _roleReadService.GetByIdAsync("GroupRead");
			var groupCrudRole = await _roleReadService.GetByIdAsync("GroupCrud");
			var studentGroupReadRole = await _roleReadService.GetByIdAsync("StudentGroupRead");
			var studentGroupCrudRole = await _roleReadService.GetByIdAsync("StudentGroupCrud");

			user.AddRole(sexReadRole);
			user.AddRole(studentReadRole);
			user.AddRole(studentCrudRole);
			user.AddRole(groupReadRole);
			user.AddRole(groupCrudRole);
			user.AddRole(studentGroupReadRole);
			user.AddRole(studentGroupCrudRole);

			await CrudService.AddAsync(user);
		}
	}
}