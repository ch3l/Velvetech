using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Services.Base.Interfaces;

namespace Velvetech.Data.Seeds.Base
{
	public abstract class Seed<TEntity, TKey> 
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		protected readonly ICrudService<TEntity, TKey> CrudService;

		protected Seed(ICrudService<TEntity, TKey> crudService)
		{
			CrudService = crudService;
		}

		protected abstract Task AddEntities();

		public async Task<bool> SeedAsync()
		{
			var count = await CrudService.CountAsync();
			if (count > 0)
				return false;

			await AddEntities();
		
			return true;
		}
	}
}
