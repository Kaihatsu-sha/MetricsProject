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
    public class DotNetMetricJob : IJob
    {
        private IRepositoryService<DotNetMetricDto> _service;
        private PerformanceCounter _perCounter;
        private readonly ILogger<DotNetMetricJob> _logger;

        public DotNetMetricJob(IRepositoryService<DotNetMetricDto> service, ILogger<DotNetMetricJob> logger)
        {
            _service = service;
            _perCounter = new PerformanceCounter("System", "Exception Dispatches/sec", "");

        }

        public async Task Execute(IJobExecutionContext context)
        {
            var s = _perCounter.NextValue();
            System.Threading.Thread.Sleep(1000);//sec

            await _service.AddAsync(new DotNetMetricDto
            {
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
                Value = Convert.ToInt32(_perCounter.NextValue())
            });

            ///return Task.CompletedTask;
        }

    }
}
