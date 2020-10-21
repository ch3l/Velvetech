using System;
using System.Threading.Tasks;

using Domain.Common;
using Domain.Services.Interfaces;

using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;

namespace Domain.Services
{
	public class ListService<TEntity> : IListService<TEntity>
		where TEntity : BaseEntity, IAggregateRoot
	{
		IAsyncRepository<TEntity> _repository;

		public ListService(IAsyncRepository<TEntity> studentRepository)
		{
			_repository = studentRepository;
		}

		public async Task<TEntity[]> GetAllAsync() =>
			await _repository.GetAllAsync();
	}
}
