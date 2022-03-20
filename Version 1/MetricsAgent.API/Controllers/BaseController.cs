using MetricsAgent.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace MetricsAgent.API.Controllers
{
    public abstract class BaseController<TController,TEntityDto> : ControllerBase
        where TController : ControllerBase
        where TEntityDto : BaseEntityDto
    {
        protected readonly ILogger<TController> _logger;
        protected readonly IRepositoryService<TEntityDto> _service;

        public BaseController(ILogger<TController> logger, IRepositoryService<TEntityDto> service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Метод для получения метрик
        /// </summary>
        /// <param name="fromTime">С какого времени</param>
        /// <param name="toTime">По какое время</param>
        /// <returns>Возвращает коллекцию метрик</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"[BaseController] fromTIme: {fromTime} toTime {toTime}");
            var conllection = _service.GetAll().Where(entity => entity.Time > fromTime.Ticks && entity.Time < toTime.Ticks).ToList();
            _logger.LogTrace($"[BaseController] selected count {conllection.Count}");
            return Ok(conllection);
        }
    }
}
