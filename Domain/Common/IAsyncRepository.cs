using System.Collections.Generic;
using System.Threading.Tasks;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(TKey id);
		Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
		Task<int> CountAsync();
    }
}
