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

            services.AddScoped<IDbRepository<CpuMetric>, DbRepository<CpuMetric>>();//����������� ��� CPU
            services.AddScoped<IRepositoryService<CpuMetricDto>, RepositoryService<CpuMetric, CpuMetricDto>>();

            services.AddScoped<IDbRepository<DotNetMetric>, DbRepository<DotNetMetric>>();//�����������
            services.AddScoped<IRepositoryService<DotNetMetricDto>, RepositoryService<DotNetMetric, DotNetMetricDto>>();
            
            services.AddScoped<IDbRepository<HddMetric>, DbRepository<HddMetric>>();//�����������
            services.AddScoped<IRepositoryService<HddMetricDto>, RepositoryService<HddMetric, HddMetricDto>>();
            
            services.AddScoped<IDbRepository<NetworkMetric>, DbRepository<NetworkMetric>>();//�����������
            services.AddScoped<IRepositoryService<NetworkMetricDto>, RepositoryService<NetworkMetric, NetworkMetricDto>>();
            
            services.AddScoped<IDbRepository<RamMetric>, DbRepository<RamMetric>>();//�����������
            services.AddScoped<IRepositoryService<RamMetricDto>, RepositoryService<RamMetric, RamMetricDto>>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ������� ������ ����� ������",
                    Description = "��� ����� �������� � api ������ �������",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kaihatsu",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/Kaihatsu-sha/MetricsManager"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "����� ������� ��� ����� ��������� ��� ������������",
                        Url = new Uri("https://example.com/license"),
                    }                   

                });
                // ��������� ���� �� �������� ����� ����������� ��� Swagger UI
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

            // ��������� middleware � �������� ��� ��������� Swagger ��������.
            app.UseSwagger();
            // ��������� middleware ��� ��������� swagger-ui 
            // ��������� Swagger JSON �������� (���� ���������� �� ��������������� �������������
            // �� ������� ����� �������� UI).
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ������� ������ ����� ������");
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
