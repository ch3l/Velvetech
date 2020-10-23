using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity, in TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
		Task<TEntity> GetByIdAsync(TKey id);
		Task<TEntity> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);

		IAsyncEnumerable<TEntity> GetAllAsync();
		IAsyncEnumerable<TEntity> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);

		IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take);
		IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);

		Task<int> CountAsync();
		Task<int> CountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);

		Task<TEntity> AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(TEntity[] entity);
		
	}
}
