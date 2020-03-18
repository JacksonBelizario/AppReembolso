using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using App.Models;
using App.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Web
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
            services.AddControllers();
            services.AddSingleton<IItemRepository, ItemRepository>();


            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            // var connectionString = "host=localhost;port=5432;database=apireembolso;username=usuario;password=usuario";
            services.AddDbContext<SistemaDbContext>(options =>
                options.UseNpgsql(
                    connectionString
                )
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}