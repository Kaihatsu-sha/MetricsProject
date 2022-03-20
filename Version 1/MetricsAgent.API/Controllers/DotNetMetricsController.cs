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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IRepositoryService<DotNetMetricDto> _service;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IRepositoryService<DotNetMetricDto> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsFromTime([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"[DotNetMetricsController] fromTIme: {fromTime} toTime {toTime}");
            var conllection = _service.GetAll().Where(entity => entity.Time > fromTime.Ticks && entity.Time < toTime.Ticks).ToList();
            _logger.LogTrace($"[DotNetMetricsController] selected count {conllection.Count}");
            return Ok(conllection);
        }
    }
}
