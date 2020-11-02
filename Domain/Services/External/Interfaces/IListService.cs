using System.Collections.Generic;
using Velvetech.Domain.Common;

namespace Velvetech.Domain.Services.External.Interfaces
{
	public interface IListService<out TEntity, TKey> where TEntity : Entity<TKey>
	{
		public IAsyncEnumerable<TEntity> ListAsync();
	}
}
