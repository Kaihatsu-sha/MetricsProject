using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.API.Controllers;
using MetricsManager.Entities;
using Microsoft.Extensions.Logging;

namespace MetricsManager.API.Tests
{
    public class AgentsControllerTests
    {
        private readonly AgentsController _controller;

        public AgentsControllerTests()
        {
            Mock<ILogger<AgentsController>> logger = new Mock<ILogger<AgentsController>>();
            _controller = new AgentsController(logger.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            AgentInfo agent = new AgentInfo();
            //Act
            var result = _controller.RegisterAgent(agentInfo: agent);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            int agentId = 1;
            //Act
            var result = _controller.EnableAgentById(agentId);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            int agentId = 1;
            //Act
            var result = _controller.DisableAgentById(agentId);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetRegisteredAgents_ReturnsOk()
        {
            //Arrange
            //Act
            var result = _controller.GetRegisteredAgents();
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        

    }
}
