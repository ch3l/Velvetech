using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Velvetech.Domain.Common;
using Velvetech.Domain.Common.Validation.Exceptions;
using Velvetech.Domain.Common.Validation.Interfaces;

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
		private readonly ILogger<EfRepository<TEntity, TKey>> _logger;

		public EfRepository(AppDbContext dbContext, 
			ILogger<EfRepository<TEntity, TKey>> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		private void CheckIfValidatable(TEntity entity)
		{
			if (entity is IValidatableEntity validatableEntity &&
				validatableEntity.HasErrors)
			{
				throw new MissedErrorsValidationProcessingException(validatableEntity);
			}
		}

		public async Task<TEntity> GetById(TKey id)
		{
			try
			{
				return await GetEntity().FindAsync(id);
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString()); 
				throw;
			}
		}

		public async Task<TEntity> FirstOrDefault(TKey id, ISpecification<TEntity> specification)
		{
			try
			{
				return await FromSpecification(specification).FirstOrDefaultAsync(x => x.Id.Equals(id));
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> condition, ISpecification<TEntity> specification)
		{
			try
			{
				return await FromSpecification(specification).FirstOrDefaultAsync(condition);

			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> condition)
		{
			try
			{
				return await GetEntity().FirstOrDefaultAsync(condition);
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public IAsyncEnumerable<TEntity> ListAsync()
		{
			try
			{
				return GetEntity().AsAsyncEnumerable();

			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public IAsyncEnumerable<TEntity> ListAsync(ISpecification<TEntity> specification)
		{
			try
			{
				return FromSpecification(specification).AsAsyncEnumerable();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			CheckIfValidatable(entity);

			try
			{
				await _dbContext.Set<TEntity>().AddAsync(entity);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}

			return entity;
		}

		public async Task UpdateAsync(TEntity entity)
		{
			CheckIfValidatable(entity);

			try
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task RemoveAsync(TEntity entity)
		{
			try
			{
				_dbContext.Set<TEntity>().Remove(entity);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task RemoveRangeAsync(TEntity[] entities)
		{
			try
			{
				_dbContext.Set<TEntity>().RemoveRange(entities);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task<int> CountAsync()
		{
			try
			{
				return await GetEntity().AsQueryable().CountAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		public async Task<int> CountAsync(ISpecification<TEntity> specification)
		{
			try
			{
				return await FromSpecification(specification).CountAsync();
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				throw;
			}
		}

		private DbSet<TEntity> GetEntity() =>
			_dbContext.Set<TEntity>();

		private IQueryable<TEntity> FromSpecification(ISpecification<TEntity> specification) =>
			new SpecificationEvaluator<TEntity>().GetQuery(GetEntity().AsQueryable(), specification);
	}
}