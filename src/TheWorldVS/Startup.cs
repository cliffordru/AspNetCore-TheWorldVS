using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using TheWorldVS.Services;
using TheWorldVS.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace TheWorldVS
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = 
                    new CamelCasePropertyNamesContractResolver();
                });

            services.AddLogging();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<WorldContext>();

            /* Imptant to note that could use services.AddScoped<WorldContextSeedData>();
             * however scope says during life of REQUEST will reuse instance of class.
             * Since only need instance once during configure, use transient so the obj
             * will be destroyed quickly 
             * services.AddSingleton - only one instance for life of web server
             * services.AddInstance - pass in constructed obj
             * --------------------------------------------------------------------------*/
            services.AddTransient<WorldContextSeedData>();
            services.AddScoped<IWorldRepository, WorldRepository>();

#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();
#else
            //services.AddScoped<IMailService, MailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, WorldContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            // Can add other providers for db, etc. In prod log level should be error or critical
            loggerFactory.AddDebug(LogLevel.Warning);

            // Important order matters - creating a chain of middleware
            // app.UseDefaultFiles(); // No longer want index.html to be serverd now that we are adding MVC support
            app.UseStaticFiles();

            app.UseMvc(); // listen for requests in MVC style           

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}", // id optional due to ?
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            seeder.EnsureSeedData();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
