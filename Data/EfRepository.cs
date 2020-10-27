using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

		private IQueryable<TEntity> GetEntity() =>
			_dbContext.Set<TEntity>();

		private IQueryable<TEntity> GetQueryableEntity(IQueryable<TEntity> source)
		{
			return source switch
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

				_ => source 
			};
		} 		
	
		public async Task<TEntity> GetByIdAsync(TKey id) => 
			await GetQueryableEntity(GetEntity()).FirstOrDefaultAsync(x => x.Id.Equals(id));

		public async Task<TEntity> GetByIdAsync(TKey id, IFilter<TEntity> filter) =>
			await filter.Filter(GetQueryableEntity(GetEntity())).FirstOrDefaultAsync(x => x.Id.Equals(id));


		public IAsyncEnumerable<TEntity> GetAllAsync() => 
			GetQueryableEntity(GetEntity()).AsAsyncEnumerable();
		public IAsyncEnumerable<TEntity> GetAllAsync(IFilter<TEntity> filter) =>
			filter.Filter(GetQueryableEntity(GetEntity())).AsAsyncEnumerable();


		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take) => 
			GetQueryableEntity(GetEntity()).Skip(skip).Take(take).AsAsyncEnumerable();

		public IAsyncEnumerable<TEntity> GetRangeAsync(int skip, int take, IFilter<TEntity> filter) => 
			filter.Filter(GetQueryableEntity(GetEntity())).Skip(skip).Take(take).AsAsyncEnumerable();

		
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
			_dbContext.Set<TEntity>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task RemoveRangeAsync(TEntity[] entities)
		{
			_dbContext.Set<TEntity>().RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<int> CountAsync() => 
			await GetEntity().CountAsync();

		public async Task<int> CountAsync(IFilter<TEntity> filter) =>
			await filter.Filter(GetEntity()).CountAsync();
	}
}