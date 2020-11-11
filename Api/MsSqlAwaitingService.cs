using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Velvetech.Api.Controllers;
using Velvetech.Data;
using Velvetech.Domain.Entities;

namespace Velvetech.Api
{
	public class MsSqlAwaitingService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<MsSqlAwaitingService> _logger;

		public MsSqlAwaitingService(IServiceProvider serviceProvider,
			ILogger<MsSqlAwaitingService> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"MSSQL awaiting background task is starting.");

			cancellationToken.Register(() =>
				_logger.LogDebug($"MSSQL awaiting background task is stopping."));

			while (!cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug($"Trying to connect to MSSQL server.");
				try
				{
					using var scope = _serviceProvider.CreateScope();

					var context =
						scope.ServiceProvider.GetRequiredService<AppDbContext>();

					await context.Database.MigrateAsync();

					if (!await context.Sex.AnyAsync())
					{
						await context.Sex.AddRangeAsync(
							new Sex("Female"),
							new Sex("Male"));

						await context.SaveChangesAsync();
					}

					StateController.State = true;
					await Task.Delay(1000, cancellationToken);
				}
				catch (Exception)
				{
					await Task.Delay(1000, cancellationToken);
				}
			}

			_logger.LogDebug($"Awaiting of MSSQL response finished successfully");
		}
	}
}