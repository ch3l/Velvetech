using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
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
