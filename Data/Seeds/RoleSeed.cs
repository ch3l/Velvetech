using System.Threading.Tasks;
using Velvetech.Data.Seeds.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;

namespace Velvetech.Data.Seeds
{
	public class RoleSeed : Seed<Role, string>
	{
		public RoleSeed(ICrudService<Role, string> crudService)
			: base(crudService)
		{
		}

		protected override async Task AddEntities()
		{
			await CrudService.AddAsync(new Role("SexRead"));
			await CrudService.AddAsync(new Role("StudentRead"));
			await CrudService.AddAsync(new Role("StudentCrud"));
			await CrudService.AddAsync(new Role("GroupRead"));
			await CrudService.AddAsync(new Role("GroupCrud"));
			await CrudService.AddAsync(new Role("StudentGroupRead"));
			await CrudService.AddAsync(new Role("StudentGroupCrud"));
		}
	}
}