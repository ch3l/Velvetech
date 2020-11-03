using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Velvetech.Domain.Common
{
    public interface IAsyncRepository<TEntity, in TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
	    Task<TEntity> GetById(TKey id);
		Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification);
		Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> condition, ISpecification<TEntity> specification);
		Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> condition);

		IAsyncEnumerable<TEntity> ListAsync();
		IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification);

		Task<int> CountAsync();
		Task<int> CountAsync(ISpecification<TEntity> specification);

		Task<TEntity> AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
		Task RemoveRangeAsync(TEntity[] entities);
		
	}
}
