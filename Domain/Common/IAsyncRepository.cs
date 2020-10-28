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
	    Task<TEntity> GetById(TKey id);
		Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification);

		IAsyncEnumerable<TEntity> ListAsync();
		IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification);

		Task<int> CountAsync();
		Task<int> CountAsync(ISpecification<TEntity> filterFunc);

		Task<TEntity> AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(TEntity[] entity);
		
	}
}
