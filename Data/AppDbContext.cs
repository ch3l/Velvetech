using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Velvetech.Domain.Entities;

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
					//.UseInMemoryDatabase("Velvetech");
					.UseSqlServer("Server=.\\sqlexpress;Database=Velvetech;Trusted_Connection=True;")
					;//.UseLazyLoadingProxies();
			}
		}

		public static async Task<AppDbContext> MakeSqlLiteAsync()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseSqlite("DataSource=:memory:")
				.Options;
			
			var context = new AppDbContext(options);
			var repository = new EfRepository<Sex, int>(context);

			await repository.AddAsync(new Sex(1, "Male"));
			await repository.AddAsync(new Sex(2, "Female"));

			return context;
		}
	}
}
