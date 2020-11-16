using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Velvetech.Api.Services;
using Velvetech.Api.Services.Background;
using Velvetech.Data;
using Velvetech.Data.Seeds;
using Velvetech.Domain.Common;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Services.External.Common;
using Velvetech.Domain.Services.External.Common.Interfaces;
using Velvetech.Domain.Services.External.Particular;
using Velvetech.Domain.Services.External.Particular.Interfaces;
using Velvetech.Domain.Services.Internal;
using Velvetech.Domain.Services.Internal.Interfaces;
using Velvetech.Shared;

namespace Velvetech.Api
{
	// Copyright (c) .NET Foundation. Licensed under the Apache License, Version 2.0.

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

			var connectionStringTag = Environment.GetEnvironmentVariable("MSSQL") ?? "WindowsMsSql";
			var connectionString = Configuration.GetConnectionString(connectionStringTag);
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
			
			services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
			services.AddScoped(typeof(IReadService<,>), typeof(ReadService<,>));
			services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));

			services.AddScoped<ICrudService<Student, Guid>, StudentCrudService>();
			services.AddScoped<ICrudService<Group, Guid>, GroupCrudService>();
			services.AddScoped<IGroupingService, GroupingService>();
			services.AddScoped<IStudentValidationService, StudentValidationService>();
			services.AddScoped<IUsersRolesService, UsersRolesService>();
			services.AddScoped<AuthorizationService>();

			//services.AddTransient(typeof(IReadService<,>), typeof(ReadService<,>));
			services.AddTransient<SexSeed>();
			services.AddTransient<RoleSeed>();
			services.AddTransient<UserSeed>();
			services.AddHostedService<MigrationAndSeedService>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = JwtShared.Issuer,

						ValidateAudience = true,
						ValidAudience = JwtShared.Audience,
						ValidateLifetime = true,

						IssuerSigningKey = JwtShared.SecurityKey,
						ValidateIssuerSigningKey = true,
					};
				});
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
				
				c.EnableAnnotations();
				
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. <br/><br/> 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      <br/><br/>Example: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});

				c.OrderActionsBy(apiDesc => apiDesc.GroupName);
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
			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.UseSwagger();
	
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}  