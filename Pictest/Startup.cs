﻿using System;
using System.IO;
using System.Text;
using Chroniton;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pictest.Middleware;
using Pictest.Persistence;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Repository;
using Pictest.Service;
using Pictest.Service.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace Pictest
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            _configuration = configuration;
            _environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MongoDbOptions>(_configuration.GetSection("MongoDB"));

            services.AddAuthentication(
                    options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    config.Events = new JwtBearerEvents();

                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidAudience = _configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]))
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IMongoDbRepository, MongoDbRepository>();

            services.AddSingleton<IContestRepository, ContestRepository>();
            services.AddSingleton<IPictureRepository, PictureRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IContestService, ContestService>();
            services.AddSingleton<IPictureService, PictureService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<ISingularity, Singularity>(serviceProvider => Singularity.Instance);

            services.AddRouting()
                .AddMvcCore()
                .AddCors()
                .AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; })
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddAuthorization(x =>
                    new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme))
                .AddDataAnnotationsLocalization()
                .AddApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Pictest API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var singularity = Singularity.Instance;
            singularity.Start();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pictest API"); });
            app.UseCors(builder => builder.AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                .WithOrigins("http://localhost:3000")
                .AllowCredentials()
                .SetPreflightMaxAge(TimeSpan.FromDays(30)));
            app.UseAuthentication();
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
                RequestPath = new PathString("/Uploads"),
                EnableDirectoryBrowsing = true
            });
            app.UseExceptionMiddleware();
            app.UseMvc();
        }
    }
}