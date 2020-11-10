using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
	public class BaseUrlConfiguration
	{
		public const string CONFIG_NAME = "baseUrls";

		public string ApiBase { get; set; }
		public string WebBase { get; set; }
	}

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

			//var connection = "Server=localhost;Database=Velvetech;User=sa;Password=Qwerty1!;";
			////var connection = "Server=.\\sqlexpress;Database=Velvetech;Trusted_Connection=True;";

			//services.AddDbContext<AppDbContext>(
			//	options => options.UseSqlServer(connection));

			//services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
			//services.AddScoped(typeof(IListService<,>), typeof(ListService<,>));
			//services.AddScoped(typeof(ICrudService<Student, Guid>), typeof(StudentCrudService));
			//services.AddScoped(typeof(ICrudService<Group, Guid>), typeof(GroupCrudService));
			//services.AddScoped(typeof(IGroupingService), typeof(GroupingService));
			//services.AddScoped(typeof(IStudentValidationService), typeof(StudentValidationService));

			var baseUrlConfig = new BaseUrlConfiguration();
			Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);

			
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
		}
	}
}
