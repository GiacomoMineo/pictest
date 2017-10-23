using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pictest.Middleware;
using Pictest.Model;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Repository;

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
            var mongodbopt = _configuration.GetSection("MongoDB").Get<MongoDbOptions>();
            services.Configure<MongoDbOptions>(_configuration.GetSection("MongoDB"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IMongoDbRepository, MongoDbRepository>();

            services.AddSingleton<ITopicRepository, TopicRepository>();

            services.AddRouting()
                .AddMvcCore()
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseJsonRequest();
            app.UseMvc();
        }
    }
}
