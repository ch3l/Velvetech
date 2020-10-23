using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.Interfaces
{
	public interface ICrudService<TEntity, in TKey> where	TEntity : Entity<TKey>
	{
		public IAsyncEnumerable<TEntity> GetAllAsync();
		public IAsyncEnumerable<TEntity> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);
		
		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take);
		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);
		
		public Task<TEntity> GetByIdAsync(Guid id);
		public Task<TEntity> GetByIdAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);

		public Task<int> CountAsync();
		public Task<int> CountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);

		public Task<TEntity> AddAsync(TEntity entity);
		public Task UpdateAsync(TEntity entity);
		public Task DeleteAsync(TKey id);
	}
}
