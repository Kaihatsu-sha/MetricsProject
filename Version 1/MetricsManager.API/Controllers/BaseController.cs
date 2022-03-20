using MetricsManager.Services;
using MetricsManager.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.API.Controllers
{
    public abstract class BaseController<TController, TEntityDto> : ControllerBase
        where TController : ControllerBase
        where TEntityDto : BaseEntityDto
    {
        protected readonly ILogger<TController> Logger;
        protected readonly IMetricService<TEntityDto> RepositoryService;

        public BaseController(ILogger<TController> logger, IMetricService<TEntityDto> service)
        {
            Logger = logger;
            RepositoryService = service;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            Logger.LogTrace($"BaseController] GetMetricsFromAgent agentId: {agentId} fromTime: {fromTime} toTime: {toTime}");
            var result = RepositoryService.GetFromAgent(agentId, fromTime, toTime);

            return Ok(result);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            Logger.LogTrace($"[BaseController] GetMetricsFromAllCluster fromTime: {fromTime} toTime: {toTime}");
            var result = RepositoryService.GetFromCluster(fromTime, toTime);

            return Ok(result);
        }
    }
}
