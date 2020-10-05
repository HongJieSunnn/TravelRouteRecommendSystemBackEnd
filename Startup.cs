using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TravelRouteRecommendSystemBackEnd.Data;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;
using TravelRouteRecommendSystemBackEnd.Services;

namespace TravelRouteRecommendSystemBackEnd
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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .WithMethods("GET");
                    });
            });

            services.AddHttpClient();
            services.AddScoped<IHttpRequestRepository, HttpRequestMapRepository>();
            services.AddScoped<IUserRequirementFromCSharp, UserRequirementFromCSharp>();
            services.AddScoped<IRecommendationsRepository, RecomendationsRepository>();

            services.AddDbContextPool<MySQLDbContext>(options => options.UseMySql(
                Configuration.GetConnectionString("ConnectMySQL"),
                mySqlOptions => mySqlOptions.ServerVersion(new Version(5, 7), ServerType.MySql)
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
