using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.Entities;
using MetricsManager.Services;
using MetricsManager.Services.Dto;

namespace MetricsManager.API.Tests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller;
        private readonly Mock<IRepositoryService<DtoCpuMetric>> _repository;
        public CpuMetricsControllerTests()
        {
            Mock<ILogger<CpuMetricsController>> logger = new Mock<ILogger<CpuMetricsController>>();
            _repository = new Mock<IRepositoryService<DtoCpuMetric>>();
            _controller = new CpuMetricsController(logger.Object, _repository.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            int agentId = 1;
            TimeSpan fromTIme = new TimeSpan(10);
            TimeSpan toTime = new TimeSpan(100);
            //Act
            var result = _controller.GetMetricsFromAgent(agentId, fromTIme, toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            TimeSpan fromTIme = new TimeSpan(10);
            TimeSpan toTime = new TimeSpan(100);
            _repository.Setup(repository => repository.GetAll()).Verifiable();
            //Act
            var result = _controller.GetMetricsFromAllCluster(fromTIme, toTime);
            //Assert
            _repository.Verify(repository => repository.AddAsync(It.IsAny<DtoCpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _repository.Setup(repository => repository.AddAsync(It.IsAny<DtoCpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new DtoCpuMetric { Time = 10, Value = 50 });
            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repository.Verify(repository => repository.AddAsync(It.IsAny<DtoCpuMetric>()), Times.AtMostOnce());
        }
    }
}
