using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace MetricsAgent.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var path = config.GetValue<string>("Logging:FilePath");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()//.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(path)
                .CreateLogger();

            IHost host = CreateHostBuilder(args).Build();
            //using (IServiceScope scope = host.Services.CreateScope())
            //{
            //    //var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    //db.Database.Migrate();
            //}
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    MetricsAgent.Services.Schedulers.QuartzScheduler scheduler = new Services.Schedulers.QuartzScheduler(services);
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
