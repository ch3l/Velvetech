using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Velvetech.Data;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities.StudentAggregate;
using Velvetech.Domain.Entities.GroupAggregate;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Velvetech.Data.Repositories
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey> 
		where TEntity : BaseEntity, IAggregateRoot 
    {
        protected readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

		private DbSet<TEntity> GetEntity() =>
			_dbContext.Set<TEntity>();

		/*
		private IQueryable<TEntity> GetEntity()
		{
			var entity = _dbContext.Set<TEntity>();
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
		*/

		public async Task<TEntity> GetByIdAsync(TKey id)
        {
			return await GetEntity().FindAsync(id);
        }

		public async Task<TEntity[]> GetAllAsync()        
		{
			return await GetEntity().ToArrayAsync();
        }

		public async Task<TEntity[]> GetRangeAsync(int skip, int take)
		{
			return await GetEntity().Skip(skip).Take(take).ToArrayAsync();
		}

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

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

		public async Task<int> CountAsync()
		{
			return await GetEntity().CountAsync();
		}
	}
}