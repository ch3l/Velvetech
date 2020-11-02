using System.Collections.Generic;
using Velvetech.Domain.Common;
using Velvetech.Domain.Services.External.Interfaces;

namespace Velvetech.Domain.Services.External
{
	public class ListService<TEntity, TKey> : IListService<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		readonly IAsyncRepository<TEntity, TKey> _repository;

		public ListService(IAsyncRepository<TEntity, TKey> studentRepository)
		{
			_repository = studentRepository;
		}

		public IAsyncEnumerable<TEntity> ListAsync() =>
			_repository.ListAsync();
	}
}
