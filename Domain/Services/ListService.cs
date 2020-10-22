using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Common;
using Domain.Services.Interfaces;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;

namespace Domain.Services
{
	public class ListService<TEntity, TKey> : IListService<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		IAsyncRepository<TEntity, TKey> _repository;

		public ListService(IAsyncRepository<TEntity, TKey> studentRepository)
		{
			_repository = studentRepository;
		}

		public IAsyncEnumerable<TEntity> GetAllAsync() =>
			_repository.GetAllAsync();
	}
}
