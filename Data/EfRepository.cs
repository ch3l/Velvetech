using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation;

namespace Velvetech.Data
{
	/// <summary>
	/// "There's some repetition here - couldn't we have some the sync methods call the async?"
	/// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
	/// </summary>
	public class EfRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		private readonly AppDbContext _dbContext;

		public EfRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		void CheckIfValidatable(TEntity entity)
		{
			if (entity is IValidatableEntity validatableEntity &&
			    validatableEntity.HasValidationErrors)
			{
				throw new ValidationException(validatableEntity);
			}
		}

		public async Task<TEntity> GetById(TKey id) =>
			await GetEntity().FindAsync(id);

		public async Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification) =>
			await FromSpecification(specification).FirstOrDefaultAsync(x => x.Id.Equals(id));

		public IAsyncEnumerable<TEntity> ListAsync() =>
			GetEntity().AsAsyncEnumerable();

		public IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification) =>
			FromSpecification(specification).AsAsyncEnumerable();

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			CheckIfValidatable(entity);

			await _dbContext.Set<TEntity>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task UpdateAsync(TEntity entity)
		{
			CheckIfValidatable(entity);

			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}

		public async Task RemoveAsync(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task RemoveRangeAsync(TEntity[] entities)
		{
			_dbContext.Set<TEntity>().RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<int> CountAsync() =>
			await GetEntity().AsQueryable().CountAsync();

		public async Task<int> CountAsync(ISpecification<TEntity> specification) =>
			await FromSpecification(specification).CountAsync();

		private DbSet<TEntity> GetEntity() =>
			_dbContext.Set<TEntity>();

		private IQueryable<TEntity> FromSpecification(ISpecification<TEntity> specification) =>
			new SpecificationEvaluator<TEntity>().GetQuery(GetEntity().AsQueryable(), specification);
	}
}