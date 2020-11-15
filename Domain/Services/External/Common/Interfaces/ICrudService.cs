using System.Threading.Tasks;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.External.Common.Interfaces
{
	public interface ICrudService<TEntity, in TKey> : IReadService<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		Task<TEntity> AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(TKey id);
	}
}
