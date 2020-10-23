using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
		Task<TEntity> GetByIdAsync(TKey id);
		IAsyncEnumerable<TEntity> GetAllAsync();
		IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take);
		IAsyncEnumerable<TEntity> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc);
		IAsyncEnumerable<TEntity> GetRangeAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterFunc, int skip, int take);
		Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(TEntity[] entity);
		Task<int> CountAsync();
    }
}
