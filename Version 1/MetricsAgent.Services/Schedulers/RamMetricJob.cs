using MetricsAgent.Services.Repository;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgent.Services.Schedulers
{
    public class RamMetricJob : IJob
    {
        private IRepositoryService<RamMetricDto> _service;
        private PerformanceCounter _perCounter;
        private readonly ILogger<RamMetricJob> _logger;

        public RamMetricJob(IRepositoryService<RamMetricDto> service, ILogger<RamMetricJob> logger)
        {
            _service = service;
            _logger = logger;
            _perCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _service.AddAsync(new RamMetricDto
            {
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
                Value = Convert.ToInt32(_perCounter.NextValue())
            });

            //return Task.CompletedTask;
        }

    }
}
