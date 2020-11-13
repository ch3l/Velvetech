using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Velvetech.Api.Controllers;
using Velvetech.Data;

namespace Velvetech.Api
{
	public class MigrationAndSeedService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<MigrationAndSeedService> _logger;

		public MigrationAndSeedService(IServiceProvider serviceProvider,
			ILogger<MigrationAndSeedService> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"MSSQL migrate and seed task is starting.");

			cancellationToken.Register(() =>
				_logger.LogDebug($"MSSQL migrate and seed task is stopping."));

			while (!cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug($"Trying to connect to MSSQL server.");
				try
				{
					using var scope = _serviceProvider.CreateScope();
					await using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
					await context.MigrateAndSeed();
					
					StateController.State = true;
					await StopAsync(cancellationToken);
				}
				catch (Exception)
				{
					await Task.Delay(1000, cancellationToken);
				}
			}

			_logger.LogDebug($"MSSQL migrate and seed task finished successfully");
		}
	}
}