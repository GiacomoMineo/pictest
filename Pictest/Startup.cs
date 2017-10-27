using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pictest.Middleware;
using Pictest.Persistence;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Repository;
using Pictest.Service;
using Pictest.Service.Interface;

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

            services.AddAuthentication()
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    config.Events = new JwtBearerEvents {};

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidAudience = _configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]))
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IMongoDbRepository, MongoDbRepository>();

            services.AddSingleton<IContestRepository, ContestRepository>();
            services.AddSingleton<IPictureRepository, PictureRepository>();

            services.AddSingleton<IContestService, ContestService>();
            services.AddSingleton<IPictureService, PictureService>();

            services.AddRouting()
                .AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddDataAnnotationsLocalization();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionMiddleware();
            app.UseMvc();
        }
    }
}
