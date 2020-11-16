using System.Threading.Tasks;

using Velvetech.Domain.Common;
using Velvetech.Domain.Services.Base.Interfaces;

namespace Velvetech.Domain.Services.Base
{
	public class CrudService<TEntity, TKey> : ReadService<TEntity, TKey>, ICrudService<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		public CrudService(IAsyncRepository<TEntity, TKey> repository)
			:base(repository)
		{
		}

		public virtual async Task<TEntity> AddAsync(TEntity entity) =>
			await _repository.AddAsync(entity);

		public virtual async Task UpdateAsync(TEntity entity) =>
			await _repository.UpdateAsync(entity);

		public virtual async Task DeleteAsync(TKey id)
		{
			var item = await _repository.GetById(id);
			if (item is null)
				return;

			await _repository.RemoveAsync(item);
		}
	}
}