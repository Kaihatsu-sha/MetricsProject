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
    public class NetworkMetricJob : IJob
    {
        private IRepositoryService<NetworkMetricDto> _service;
        private PerformanceCounter _perCounter;
        private readonly ILogger<NetworkMetricJob> _logger;

        public NetworkMetricJob(IRepositoryService<NetworkMetricDto> service, ILogger<NetworkMetricJob> logger)
        {
            _service = service;
            _logger = logger;
            _perCounter = new PerformanceCounter(categoryName: "Network Interface", counterName: "Bytes Total/sec", instanceName: "Intel[R] Wireless-AC 9560 160MHz");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var s = _perCounter.NextValue();
            System.Threading.Thread.Sleep(1000);//sec

            await _service.AddAsync(new NetworkMetricDto
            {
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
                Value = Convert.ToInt32(_perCounter.NextValue())
            });
            //return Task.CompletedTask;
        }

    }
}
