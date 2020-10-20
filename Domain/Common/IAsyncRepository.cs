using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity, TKey> where TEntity :  IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(TKey id);
		Task<TEntity[]> GetAllAsync();
		Task<TEntity[]> GetRangeAsync(int skip, int take);
		Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
		Task<int> CountAsync();
    }
}
