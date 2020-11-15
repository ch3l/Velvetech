using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Velvetech.Data.Seeds;
using Velvetech.Domain.Entities;

namespace Velvetech.Data
{
	public static class Extensions
	{
		public static async Task MigrateAndSeed(this IServiceProvider serviceProvider)
		{
			await using var context = serviceProvider.GetRequiredService<AppDbContext>();
			await context.Database.MigrateAsync();

			var sexSeed = serviceProvider.GetRequiredService<SexSeed>();
			var sexSeedIsDone = await sexSeed.SeedAsync();

			var roleSeed = serviceProvider.GetRequiredService<RoleSeed>();
			var roleSeedIsDone = await roleSeed.SeedAsync();

			var userSeed = serviceProvider.GetRequiredService<UserSeed>();
			var userSeedIsDone = await userSeed.SeedAsync();
			var w = userSeedIsDone;
		}
	}
}
