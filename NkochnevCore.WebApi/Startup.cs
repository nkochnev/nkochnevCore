﻿using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NkochnevCore.Infrastructure;
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
            services.AddMvc(config => { config.Filters.Add(typeof(ExceptionFilter)); });
            services.AddCors();

            services.AddDbContext<NkochnevDataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("NkochnevDataContext")));
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.Issuer,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.Audience,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // странная опция
                        // можно указать период, когда токен истёк, но считается сервером валидным
                        ClockSkew = TimeSpan.FromMinutes(5),

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute("spa-fallback", new {controller = "Home", action = "Index"});
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            InitializeDatabase(app);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<NkochnevDataContext>().Database.Migrate();
            }
        }
    }
}