using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolPerformance.Models;
using StackExchange.Redis;
using SchoolPerformance.Repository;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using SchoolPerformance.Cache;

namespace SchoolPerformance
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<SchoolPerformanceContext>(options =>
                    options.UseSqlServer(
                                _configuration.GetConnectionString("SchoolPerformanceAzure")));

            services.AddMvc().AddNewtonsoftJson();

            //Register Redis services
            var redisConfiguration = _configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            //Register repository
            services.AddScoped(typeof( ISchoolPerformanceRepository <>),typeof(SchoolPerformanceRepository<>));

            //Register RedisCache Class
            services.AddSingleton<IRedisCache,RedisCache>();

            services.AddScoped<AutoCompleteService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
