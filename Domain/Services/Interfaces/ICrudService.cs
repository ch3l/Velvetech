using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Common;

namespace Domain.Services.Interfaces
{
	public interface ICrudService<TEntity, TKey> where	TEntity : Entity<TKey>
	{
		public IAsyncEnumerable<TEntity> GetAllAsync();
		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take);
		public Task<TEntity> GetByIdAsync(Guid id);
		public Task<TEntity> AddAsync(TEntity entity);
		public Task UpdateAsync(TEntity entity);
		public Task DeleteAsync(TKey id);
		public Task<int> CountAsync();
	}
}
