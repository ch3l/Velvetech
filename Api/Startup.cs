using System;
using System.Data.Common;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Velvetech.Data;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External;
using Velvetech.Domain.Services.External.Interfaces;
using Velvetech.Domain.Services.Internal;
using Velvetech.Domain.Services.Internal.Interfaces;

namespace Velvetech.Api
{

	public class Startup
	{
		private const string CORS_POLICY = "CorsPolicy";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			
			services.AddDbContext<AppDbContext>(
				options => options.UseSqlServer(Configuration.GetConnectionString("InDockerMsSql")));
				//options => options.UseSqlServer(Configuration.GetConnectionString("OutOfDockerMsSql")));

			services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
			services.AddScoped(typeof(IListService<,>), typeof(ListService<,>));
			services.AddScoped(typeof(ICrudService<Student, Guid>), typeof(StudentCrudService));
			services.AddScoped(typeof(ICrudService<Group, Guid>), typeof(GroupCrudService));
			services.AddScoped(typeof(IGroupingService), typeof(GroupingService));
			services.AddScoped(typeof(IStudentValidationService), typeof(StudentValidationService));
		

			services.AddCors(options =>
			{
				options.AddPolicy(name: CORS_POLICY,
					builder =>
					{
						builder.AllowAnyOrigin();
						//builder.WithOrigins(baseUrlConfig.WebBase.Replace("host.docker.internal", "localhost").TrimEnd('/'));
						builder.AllowAnyMethod();
						builder.AllowAnyHeader();
					});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseHttpsRedirection();
			//app.UseStaticFiles();
			app.UseCors(CORS_POLICY);

			app.UseRouting();
			//app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//Thread.Sleep(15000);

			UpdateDatabase(app);
		}

		private static void UpdateDatabase(IApplicationBuilder app)
		{
			while (true)
			{
				try
				{
					using var serviceScope = app.ApplicationServices
						.GetRequiredService<IServiceScopeFactory>()
						.CreateScope();

					using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

					context.Database.Migrate();

					if (!context.Sex.Any())
					{
						context.Sex.AddRange(
							new Sex("Female"),
							new Sex("Male"));

						context.SaveChanges();
					}

					break;
				}
				catch (Exception)
				{
					Thread.Sleep(1000);
				}
			}
		}
	}
}
