using MetricsAgent.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.API.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : BaseController<CpuMetricsController, CpuMetricDto>
    {

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IRepositoryService<CpuMetricDto> service)
            : base(logger, service)
        {
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateMetrics()
        {
            await _service.AddAsync(new CpuMetricDto
            {
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()).Ticks,
                Value = 1023//Convert.ToInt32(_perCounter.NextValue())
            });
            return Ok();
        }
    }
}
