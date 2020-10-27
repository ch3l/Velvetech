using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.Interfaces
{
	public interface ICrudService<TEntity, in TKey> where	TEntity : Entity<TKey>
	{
		public IAsyncEnumerable<TEntity> GetAllAsync();
		public IAsyncEnumerable<TEntity> GetAllAsync(IFilter<TEntity> filter, ISpecification<TEntity> specification);
		
		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take);
		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, IFilter<TEntity> filter,
			ISpecification<TEntity> specification);
		
		public Task<TEntity> GetByIdAsync(Guid id);
		public Task<TEntity> GetByIdAsync(Guid id, IFilter<TEntity> filter);

		public Task<int> CountAsync();
		public Task<int> CountAsync(IFilter<TEntity> filter);

		public Task<TEntity> AddAsync(TEntity entity);
		public Task UpdateAsync(TEntity entity);
		public Task DeleteAsync(TKey id);
	}
}
