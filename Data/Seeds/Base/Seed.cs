using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Common;
using Velvetech.Domain.Services.External.Common.Interfaces;

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

		protected abstract IEnumerable<TEntity> Enumerate();

		public async Task<bool> SeedAsync()
		{
			if (await CrudService.CountAsync() > 0)
				return false;

			foreach (var entity in Enumerate())
				await CrudService.AddAsync(entity);

			return true;
		}
	}
}
