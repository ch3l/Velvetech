using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Common;

namespace Domain.Services.Interfaces
{
	public interface ICrudService<TEntity, TKey> where	TEntity : IEntity<TKey>
	{
		public Task<TEntity[]> GetAllAsync();
		public Task<TEntity[]> GetRangeAsync(int skip, int take);
		public Task<TEntity> GetByIdAsync(Guid id);
		public Task<TEntity> AddAsync(TEntity entity);
		public Task UpdateAsync(TEntity entity);
		public Task DeleteAsync(TKey id);
	}
}
