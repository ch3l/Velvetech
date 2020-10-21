using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
    {
		IQueryable<TEntity> GetEntity();
		Task<TEntity> GetByIdAsync(params object[] id);
		IAsyncEnumerable<TEntity> GetAllAsync();
		IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take);
		Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(TEntity[] entity);
		Task<int> CountAsync();
    }
}
