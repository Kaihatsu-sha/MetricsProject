using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Entities;
using Microsoft.Extensions.Logging;
using MetricsManager.Services.Repository;

namespace MetricsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IRepositoryService<AgenDto> _repositoryService;

        public AgentsController(ILogger<AgentsController> logger, IRepositoryService<AgenDto> service)
        {
            _logger = logger;
            _repositoryService = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAgent([FromBody] AgenDto agentInfo)
        {
            _logger.LogTrace($"RegisterAgent {agentInfo.Name} {agentInfo.Address}");
            await _repositoryService.AddAsync(agentInfo);

            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public async Task<IActionResult> EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"EnableAgentById {agentId}");
            await _repositoryService.EnableAsync(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public async Task<IActionResult> DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"DisableAgentById {agentId}");            
            await _repositoryService.DisableAsync(agentId);
            return Ok();
        }

        [HttpGet("registered")]
        public async Task<IActionResult> GetRegisteredAgents()
        {
            _logger.LogTrace($"GetRegisteredAgents");
            var collection = _repositoryService.GetAll();
            return Ok(collection);
        }
    }
}
