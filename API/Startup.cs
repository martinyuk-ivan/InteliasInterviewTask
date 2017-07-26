using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Logic;
using DataAccessLayer.Model;
using DataAccessLayer.Logic.Implementation;

namespace API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add in dependency injection dbContext
            services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("UserDbConnection")));
            //add in dependency injection paging manager
            services.AddScoped<IPageManager<User>, PageManager<User>>();
            //add in dependency repository
            services.AddScoped<IRepositoryBase<User, int>, RepositoryUser>();
            //add in dependency UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMvc();
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=User}/{action=GetPage}");
            });
        }
    }
}
