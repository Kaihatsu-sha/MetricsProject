using MetricsAgent.Services.Repository;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgent.Services.Schedulers
{
    public class HddMetricJob : IJob
    {
        private IRepositoryService<HddMetricDto> _service;
        //private PerformanceCounter _perCounter;
        private readonly ILogger<HddMetricJob> _logger;

        public HddMetricJob(IRepositoryService<HddMetricDto> service, ILogger<HddMetricJob> logger)
        {
            _service = service;
            _logger = logger;
            //_perCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            double TotalDiskSize = 0;
            double AvailableDiskSize = 0;
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    TotalDiskSize += drive.TotalSize;
                    AvailableDiskSize += drive.AvailableFreeSpace;
                }
                AvailableDiskSize = AvailableDiskSize / 1_000_000;
            }
 
            await _service.AddAsync(new HddMetricDto
            {
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
                Value = Convert.ToInt32(AvailableDiskSize)
            });
            //return Task.CompletedTask;
        }

    }
}
