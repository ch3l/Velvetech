using System.Collections.Generic;
using System.Threading.Tasks;

using Ardalis.Specification;

using Velvetech.Domain.Common;
using Velvetech.Domain.Services.Base.Interfaces;

namespace Velvetech.Domain.Services.Base
{
	public class ReadService<TEntity, TKey> : IReadService<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		protected readonly IAsyncRepository<TEntity, TKey> _repository;

		public ReadService(IAsyncRepository<TEntity, TKey> studentRepository)
		{
			_repository = studentRepository;
		}

		public async Task<TEntity> GetByIdAsync(TKey id) =>
			await _repository.GetById(id);

		public async Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification) =>
			await _repository.FirstOrDefault(id, specification);


		public IAsyncEnumerable<TEntity> ListAsync() =>
			_repository.ListAsync();

		public IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification) =>
			_repository.ListAsync(specification);


		public async Task<int> CountAsync() =>
			await _repository.CountAsync();

		public async Task<int> CountAsync(ISpecification<TEntity> specification) =>
			await _repository.CountAsync(specification);
	}
}
