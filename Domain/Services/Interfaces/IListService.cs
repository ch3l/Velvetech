using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Velvetech.Domain.Common;

namespace Domain.Services.Interfaces
{
	public interface IListService<TEntity> where TEntity : BaseEntity
	{
		public Task<TEntity[]> GetAllAsync();
		public Task<int> CountAsync();
	}
}
