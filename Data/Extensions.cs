using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Velvetech.Domain.Entities;

namespace Velvetech.Data
{
	public static class Extensions
	{
		public static async Task MigrateAndSeed(this AppDbContext context)
		{
			await context.Database.MigrateAsync();

			if (!await context.Sex.AnyAsync())
			{
				await context.Sex.AddRangeAsync(
					new Sex(1, "Female"),
					new Sex(2, "Male"));

				await context.SaveChangesAsync();
			}
		}
	}
}
