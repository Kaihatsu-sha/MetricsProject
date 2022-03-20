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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IRepositoryService<HddMetricDto> _service;

        public HddMetricsController(ILogger<HddMetricsController> logger, IRepositoryService<HddMetricDto> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableSpace([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"[HddMetricsController] fromTIme: {fromTime} toTime {toTime}");
            var conllection = _service.GetAll().Where(entity => entity.Time > fromTime.Ticks && entity.Time < toTime.Ticks).ToList();
            _logger.LogTrace($"[HddMetricsController] selected count {conllection.Count}");
            return Ok(conllection);
        }
    }
}
