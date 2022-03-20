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
    public class CpuMetricJob : IJob
    {
        private IRepositoryService<CpuMetricDto> _service;
        private PerformanceCounter _perCounter;
        private readonly ILogger<CpuMetricJob> _logger;


        public CpuMetricJob(IRepositoryService<CpuMetricDto> service, ILogger<CpuMetricJob> logger)
        {
            _service = service;
            _perCounter = new PerformanceCounter(categoryName: "Processor", counterName: "% Processor Time", instanceName: "_Total");

            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var s = _perCounter.NextValue();
            System.Threading.Thread.Sleep(1000);//sec

            await _service.AddAsync(new CpuMetricDto
            {
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
                Value = Convert.ToInt32(_perCounter.NextValue())
            });

            //Task.Run(
            //    async () =>
            //    {
            //        await _service.AddAsync(new BaseDtoEntity
            //        {
            //            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
            //            Value = 1023//Convert.ToInt32(_perCounter.NextValue())
            //        });
            //    }
            //);

            //_service.AddAsync(new BaseDtoEntity
            //{
            //    Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
            //    Value = 1023//Convert.ToInt32(_perCounter.NextValue())
            //});

            //return Task.CompletedTask;
        }

    }
}