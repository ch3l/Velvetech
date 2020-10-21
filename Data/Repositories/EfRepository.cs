using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Velvetech.Domain.Common;

namespace Velvetech.Data.Repositories
{
	/// <summary>
	/// "There's some repetition here - couldn't we have some the sync methods call the async?"
	/// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EfRepository<TEntity> : IAsyncRepository<TEntity>
		where TEntity : BaseEntity, IAggregateRoot
	{
		protected readonly AppDbContext _dbContext;

		public EfRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		private DbSet<TEntity> GetTargetEntity() =>
			_dbContext.Set<TEntity>();

		/*
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

		void NavigationLoad(IEnumerable<NavigationEntry> navigations)
		{
			foreach (var navigation in navigations)
			{
				if (navigation.IsLoaded)
					continue;
				navigation.Load();

				NavigationLoad(_dbContext.Entry(navigation.EntityEntry).Navigations);
			}
		}	  
		*/

		public IQueryable<TEntity> GetEntity() => GetTargetEntity();

		public async Task<TEntity> GetByIdAsync(params object[] id)
		{
			return await GetTargetEntity().FindAsync(id);

			/*
			var entity = GetEntity();

			var entry = await entity.FindAsync(id);
			if (entry is null)
				return await Task.FromResult<TEntity>(null);

			NavigationLoad(_dbContext.Entry(entry).Navigations);

			return entry;
			*/
		}

		public async Task<TEntity[]> GetAllAsync()
		{
			return await GetTargetEntity().ToArrayAsync();
		}

		public async Task<TEntity[]> GetRangeAsync(int skip, int take)
		{
			return await GetTargetEntity().Skip(skip).Take(take).ToArrayAsync();
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

		public async Task<int> CountAsync()
		{
			return await GetTargetEntity().CountAsync();
		}
	}
}