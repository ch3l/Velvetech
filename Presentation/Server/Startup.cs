using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Velvetech.Data;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services;
using Velvetech.Domain.Services.Interfaces;

namespace Velvetech.Presentation.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			//.AddNewtonsoftJson(options => 
			//	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

			services.AddDbContext<AppDbContext>();

			services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
			services.AddScoped(typeof(IListService<,>), typeof(ListService<,>));
			services.AddScoped(typeof(ICrudService<Student, Guid>), typeof(StudentCrudService));
			services.AddScoped(typeof(ICrudService<Group, Guid>), typeof(GroupCrudService));
			services.AddScoped(typeof(IGroupingService), typeof(GroupingService));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
