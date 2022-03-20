using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgent.Services.Schedulers
{
    public class QuartzScheduler
    {
        public QuartzScheduler(IServiceCollection services)
        {
            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                AddJobs(q);

            });

            // Add the Quartz.NET hosted service

            services.AddQuartzHostedService
             (
                 q => q.WaitForJobsToComplete = true
             );

            // other config
        }

        private void AddJobs(IServiceCollectionQuartzConfigurator quartzConfigurator)
        {
            //CPU
            // Create a "key" for the job
            JobKey jobKeyCpu = new JobKey("CpuMetricJob");

            // Register the job with the DI container
            quartzConfigurator.AddJob<CpuMetricJob>(opts => opts.WithIdentity(jobKeyCpu));

            // Create a trigger for the job
            quartzConfigurator.AddTrigger(opts => opts
                .ForJob(jobKeyCpu) // JobKey link to the jobKeyCpu
                .WithIdentity("CpuJob-trigger") // give the trigger a unique name
                .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds

            //RAM
            var jobKeyRam = new JobKey("RamMetricJob");
            quartzConfigurator.AddJob<RamMetricJob>(opts => opts.WithIdentity(jobKeyRam));
            quartzConfigurator.AddTrigger(opts => opts
                .ForJob(jobKeyRam)
                .WithIdentity("RamJob-trigger")
                .WithCronSchedule("0/5 * * * * ?"));

            //DotNet
            var jobKeyDotNet = new JobKey("DotNetMetricJob");
            quartzConfigurator.AddJob<DotNetMetricJob>(opts => opts.WithIdentity(jobKeyDotNet));
            quartzConfigurator.AddTrigger(opts => opts
                .ForJob(jobKeyDotNet)
                .WithIdentity("DotNetJob-trigger")
                .WithCronSchedule("0/5 * * * * ?"));

            //HDD
            var jobKeyHdd = new JobKey("HddMetricJob");
            quartzConfigurator.AddJob<HddMetricJob>(opts => opts.WithIdentity(jobKeyHdd));
            quartzConfigurator.AddTrigger(opts => opts
                .ForJob(jobKeyHdd)
                .WithIdentity("HddJob-trigger")
                .WithCronSchedule("0/5 * * * * ?"));

            //Network
            var jobKeyNetwork = new JobKey("NetworkMetricJob");
            quartzConfigurator.AddJob<NetworkMetricJob>(opts => opts.WithIdentity(jobKeyNetwork));
            quartzConfigurator.AddTrigger(opts => opts
                .ForJob(jobKeyNetwork)
                .WithIdentity("NetworkJob-trigger")
                .WithCronSchedule("0/5 * * * * ?"));
        }
    }
}
