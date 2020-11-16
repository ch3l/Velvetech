using System.Collections.Generic;
using System.Threading.Tasks;

using Ardalis.Specification;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.Base.Interfaces
{
	public interface IReadService<TEntity, in TKey> 
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		Task<TEntity> GetByIdAsync(TKey id);
		Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification);

		IAsyncEnumerable<TEntity> ListAsync();
		IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification);

		Task<int> CountAsync();
		Task<int> CountAsync(ISpecification<TEntity> filter);
	}
}
