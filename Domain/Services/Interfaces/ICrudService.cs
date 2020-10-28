using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ardalis.Specification;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.Interfaces
{
	public interface ICrudService<TEntity, in TKey>  where TEntity : Entity<TKey>
	{
		public Task<TEntity> GetByIdAsync(Guid id);
		public Task<TEntity> GetByIdAsync(Guid id, ISpecification<TEntity> specification);

		public IAsyncEnumerable<TEntity> ListAsync();
		public IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification);

		public Task<int> CountAsync();
		public Task<int> CountAsync(ISpecification<TEntity> filter);

		public Task<TEntity> AddAsync(TEntity entity);
		public Task UpdateAsync(TEntity entity);
		public Task DeleteAsync(TKey id);
	}
}
