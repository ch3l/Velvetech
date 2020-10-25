using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;

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

		private DbSet<TEntity> GetTargetEntity() =>
			_dbContext.Set<TEntity>();

		private IQueryable<TEntity> GetQueryableEntity()
		{
			var entity = GetEntity();
			return entity switch
			{
				IQueryable<Student> student => student
					.Include(s => s.Sex) 
					.Include(s => s.Grouping)
						.ThenInclude(g => g.Group)
					.Cast<TEntity>(),

				IQueryable<Group> group => group
					.Include(s => s.Grouping)
						.ThenInclude(g => g.Student)
					.Cast<TEntity>(),

				_ => entity
			};
		} 		

		public IQueryable<TEntity> GetEntity() => 
			GetTargetEntity();

		public async Task<TEntity> GetByIdAsync(TKey id) => 
			await GetQueryableEntity().FirstOrDefaultAsync(x => x.Id.Equals(id));

		public async Task<TEntity> GetByIdAsync(TKey id, IFilter<TEntity> filterFunc) =>
			await filterFunc.Filter(GetQueryableEntity()).FirstOrDefaultAsync(x => x.Id.Equals(id));


		public IAsyncEnumerable<TEntity> GetAllAsync() => 
			GetQueryableEntity().AsAsyncEnumerable();
		public IAsyncEnumerable<TEntity> GetAllAsync(IFilter<TEntity> filterFunc) =>
			filterFunc.Filter(GetQueryableEntity()).AsAsyncEnumerable();


		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take) => 
			GetQueryableEntity().AsAsyncEnumerable().Skip(skip).Take(take).AsAsyncEnumerable();

		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, IFilter<TEntity> filterFunc) => 
			filterFunc.Filter(GetQueryableEntity()).Skip(skip).Take(take).AsAsyncEnumerable();

		
		public async Task<TEntity> AddAsync(TEntity entity)
		{
			await _dbContext.Set<TEntity>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task UpdateAsync(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;

			await _dbContext.SaveChangesAsync();			
		}

		public async Task RemoveAsync(TEntity entity)
		{
			var result = _dbContext.Set<TEntity>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task RemoveRangeAsync(TEntity[] entities)
		{
			_dbContext.Set<TEntity>().RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<int> CountAsync() => 
			await GetTargetEntity().AsAsyncEnumerable().CountAsync();

		public async Task<int> CountAsync(IFilter<TEntity> filterFunc) =>
			await filterFunc.Filter(GetTargetEntity()).AsAsyncEnumerable().CountAsync();
	}
}