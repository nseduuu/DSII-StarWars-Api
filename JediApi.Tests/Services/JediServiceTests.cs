using Castle.Core.Resource;
using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;
using System.Xml.Linq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {
            int expectedJedi = 1;
            Jedi jedi = new Jedi { Id = 1, Name = "Luke Skywalker", Strength = 100, Version = 1 };

            _repositoryMock.Setup(repository => repository.GetByIdAsync(expectedJedi)).ReturnsAsync(jedi);
            var resultJedi = await _service.GetByIdAsync(expectedJedi);

            Assert.NotNull(resultJedi);
            Assert.Equal(1, resultJedi.Id);
            Assert.Equal("Luke Skywalker", resultJedi.Name);
            Assert.Equal(100, resultJedi.Strength);
            Assert.Equal(1, resultJedi.Version);
        }


        [Fact]
        public async Task GetById_NotFound()
        {
            int expectedJedi = 2;
            Jedi jedi = new Jedi { Id = 1, Name = "Luke Skywalker", Strength = 100, Version = 1 };

            _repositoryMock.Setup(repository => repository.GetByIdAsync(expectedJedi)).ReturnsAsync(jedi);
            var resultJedi = await _service.GetByIdAsync(expectedJedi);

            Assert.Null(resultJedi);
        }

        [Fact]
        public async Task GetAll()
        {
            List<Jedi> jedis = new List<Jedi>
            {
                new Jedi { Id = 1, Name = "Luke Skywalker", Strength = 100, Version = 1 },
                new Jedi { Id = 2, Name = "Yoda", Strength = 100, Version = 1 },
                new Jedi { Id = 3, Name = "Obi Wan", Strength = 100, Version = 1 }
            };

            _repositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(jedis);
            var resultJedi = await _service.GetAllAsync();

            Assert.Equal(3, resultJedi.Count);
        }

    }
}
