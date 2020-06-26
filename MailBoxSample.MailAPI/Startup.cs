using FluentValidation;
using MailBoxSample.APIHelper.Builders;
using MailBoxSample.MailAPI.Configurations;
using MailBoxSample.MailAPI.Entities;
using MailBoxSample.MailAPI.Models;
using MailBoxSample.MailAPI.Repositories;
using MailBoxSample.MailAPI.Services;
using MailBoxSample.MailAPI.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace MailBoxSample.MailAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true);
            Configuration = builder.Build();

        }
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "MailBoxSample.MailAPI");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Basic Authentication",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
                c.SwaggerDoc(name: "v1", new OpenApiInfo() { Title = "MailBoxSample.MailAPI", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<Func<Type, UserService>>(serviceProvider => typeKey =>
            {
                if (typeKey == typeof(UserService))
                {
                    return serviceProvider.GetService<UserService>();
                }
                return null;
            });
            services.Configure<DatabaseConfiguration>(opt => Configuration.GetSection("DatabaseConfiguration").Bind(opt));
            services.AddSingleton((ILogger)new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger());

            services.AddTransient<DapperQueryBuilder<UserEntity>>();
            services.AddTransient<UserRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<AbstractValidator<UserModel>, UserModelValidator>();

            services.AddTransient<DapperQueryBuilder<MailEntity>>();
            services.AddTransient<MailRepository>();
            services.AddTransient<MailService>();
            services.AddTransient<AbstractValidator<MailModel>, MailModelValidator>();
        }
    }
}