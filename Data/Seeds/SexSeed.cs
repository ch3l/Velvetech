using System.Threading.Tasks;

using Velvetech.Data.Seeds.Base;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common.Interfaces;

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
}
