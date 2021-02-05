using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Velvetech.Domain.Entities;

namespace Velvetech.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		//public DbSet<Student> Student { get; set; }
		//public DbSet<Sex> Sex { get; set; }			
		//public DbSet<Group> Group { get; set; }
		//public DbSet<Grouping> Grouping { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
