using System.Collections.Generic;

using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.Interfaces
{
	public interface IListService<TEntity, TKey> where TEntity : Entity<TKey>
	{
		public IAsyncEnumerable<TEntity> GetAllAsync();
	}
}
