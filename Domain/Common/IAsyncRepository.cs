using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity, in TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
		Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification);
		Task<TEntity> FirstOrDefault(TKey id, IFilter<TEntity> filterFunc, ISpecification<TEntity> specification);

		IAsyncEnumerable<TEntity> GetAllAsync();
		IAsyncEnumerable<TEntity> GetAllAsync(ISpecification<TEntity> specification);
		IAsyncEnumerable<TEntity> GetAllAsync(IFilter<TEntity> filterFunc, ISpecification<TEntity> specification);

		IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, ISpecification<TEntity> specification);
		IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, IFilter<TEntity> filterFunc, ISpecification<TEntity> specification);

		Task<int> CountAsync();
		Task<int> CountAsync(IFilter<TEntity> filterFunc);

		Task<TEntity> AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(TEntity[] entity);
		
	}
}
