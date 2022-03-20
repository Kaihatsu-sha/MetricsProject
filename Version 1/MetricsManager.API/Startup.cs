using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MetricsManager.Database;
using MetricsManager.Entities;
using Microsoft.EntityFrameworkCore;
using MetricsManager.Services.Repository;
using MetricsManager.Services;

namespace MetricsManager.API
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDbRepository<AgentInfo>, DbRepository<AgentInfo>>();//Репозиторий для AgentInfo
            services.AddScoped<IRepositoryService<AgenDto>, RepositoryService<AgentInfo, AgenDto>>();
            //
            services.AddHttpClient();
            services.AddScoped<IMetricService<CpuMetricDto>, MetricService<CpuMetric, CpuMetricDto>>();
            services.AddScoped<IMetricService<DotNetMetricDto>, MetricService<DotNetMetric, DotNetMetricDto>>();
            services.AddScoped<IMetricService<HddMetricDto>, MetricService<HddMetric, HddMetricDto>>();
            services.AddScoped<IMetricService<NetworkMetricDto>, MetricService<NetworkMetric, NetworkMetricDto>>();
            services.AddScoped<IMetricService<RamMetricDto>, MetricService<CpuMetric, RamMetricDto>>();

            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
