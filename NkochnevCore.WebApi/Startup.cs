﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NkochnevCore.Infrastructure.Data;
using NkochnevCore.Infrastructure.Services;
using NkochnevCore.Infrastructure.Services.Interfaces;

namespace NkochnevCore.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddCors();

			services.AddDbContext<NkochnevDataContext>(
				options => options.UseSqlServer(Configuration.GetConnectionString("NkochnevDataContext")));
			services.AddTransient<IArticleService, ArticleService>();
			services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IEncryptionService, EncryptionService>();
			services.AddTransient<IDbContext, NkochnevDataContext>();

			services.Configure<FormOptions>(options =>
			{
				//ограничение размера файла - 60 мегабайт
				options.MultipartBodyLengthLimit = 60000000;
				//количество символов в post запросе
				options.KeyLengthLimit = 50000;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
				builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); 

			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();
		}
	}
}
