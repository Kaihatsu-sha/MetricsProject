using MetricsAgent.Database;
using MetricsAgent.Entities;
using MetricsAgent.Services;
using MetricsAgent.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MetricsAgent.API
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

            services.AddScoped<IDbRepository<CpuMetric>, DbRepository<CpuMetric>>();//Репозиторий для CPU
            services.AddScoped<IRepositoryService<CpuMetricDto>, RepositoryService<CpuMetric, CpuMetricDto>>();

            services.AddScoped<IDbRepository<DotNetMetric>, DbRepository<DotNetMetric>>();//Репозиторий
            services.AddScoped<IRepositoryService<DotNetMetricDto>, RepositoryService<DotNetMetric, DotNetMetricDto>>();
            
            services.AddScoped<IDbRepository<HddMetric>, DbRepository<HddMetric>>();//Репозиторий
            services.AddScoped<IRepositoryService<HddMetricDto>, RepositoryService<HddMetric, HddMetricDto>>();
            
            services.AddScoped<IDbRepository<NetworkMetric>, DbRepository<NetworkMetric>>();//Репозиторий
            services.AddScoped<IRepositoryService<NetworkMetricDto>, RepositoryService<NetworkMetric, NetworkMetricDto>>();
            
            services.AddScoped<IDbRepository<RamMetric>, DbRepository<RamMetric>>();//Репозиторий
            services.AddScoped<IRepositoryService<RamMetricDto>, RepositoryService<RamMetric, RamMetricDto>>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API сервиса агента сбора метрик",
                    Description = "Тут можно поиграть с api нашего сервиса",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kaihatsu",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/Kaihatsu-sha/MetricsManager"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "можно указать под какой лицензией все опубликовано",
                        Url = new Uri("https://example.com/license"),
                    }                   

                });
                // Указываем файл из которого брать комментарии для Swagger UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Включение middleware в пайплайн для обработки Swagger запросов.
            app.UseSwagger();
            // включение middleware для генерации swagger-ui 
            // указываем Swagger JSON эндпоинт (куда обращаться за сгенерированной спецификацией
            // по которой будет построен UI).
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса агента сбора метрик");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
