using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Velvetech.Domain.Entities.StudentAggregate;
using Velvetech.Domain.Entities.GroupAggregate;

namespace Velvetech.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}


		public DbSet<Student> Student { get; set; }
		public DbSet<Sex> Sex { get; set; }			
		public DbSet<Group> Group { get; set; }
		public DbSet<Grouping> Grouping { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
		{
			if (!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				optionsBuilder
					.UseSqlServer("Server=.\\sqlexpress;Database=Velvetech;Trusted_Connection=True;")
					;//.UseLazyLoadingProxies();
			}
		}
	}
}
