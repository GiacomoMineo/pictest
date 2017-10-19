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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<MongoDbOptions>(_configuration.GetSection("MongoDB"));

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
