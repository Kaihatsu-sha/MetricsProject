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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRepositoryService<RamMetricDto> _service;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRepositoryService<RamMetricDto> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableMemory([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"[RamMetricsController] fromTIme: {fromTime} toTime {toTime}");
            var conllection = _service.GetAll().Where(entity => entity.Time > fromTime.Ticks && entity.Time < toTime.Ticks).ToList();
            _logger.LogTrace($"[RamMetricsController] selected count {conllection.Count}");
            return Ok(conllection);
        }
    }
}
