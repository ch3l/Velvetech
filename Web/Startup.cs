using System;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Velvetech.Web.Services;

namespace Velvetech.Web
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
			services.AddServerSideBlazor();

			services.AddHttpClient<StateService>(ConfigureApiHttpClient)
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler(GetRetryPolicy());

			services.AddHttpClient<StudentService>(ConfigureApiHttpClient)
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler(GetRetryPolicy());

			services.AddHttpClient<GroupService>(ConfigureApiHttpClient)
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler(GetRetryPolicy());
		}

		private void ConfigureApiHttpClient(HttpClient client)
		{
			var targetApiUrl = Environment.GetEnvironmentVariable("APIURL");
			client.BaseAddress = new Uri(Configuration[targetApiUrl]);
		}

		static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
				.WaitAndRetryAsync(100, _ => TimeSpan.FromSeconds(2));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapControllers();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
