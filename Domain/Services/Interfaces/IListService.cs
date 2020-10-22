using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Common;

namespace Domain.Services.Interfaces
{
	public interface IListService<TEntity, TKey> where TEntity : Entity<TKey>
	{
		public IAsyncEnumerable<TEntity> GetAllAsync();
	}
}
