using Codebridge.Models;
using Codebridge.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreRateLimit;
using AutoMapper;
using Codebridge.Codebridge.BLL.Repository;
using Codebridge.Data;

namespace Codebridge.Tests
{
    [TestFixture]
    public class DogServiceTests
    {
        private Mock<IDogRepository> _repositoryMock;
        private Mock<IIpPolicyStore> _policyStoreMock;
        private DogService _dogService;
        private Mock<IMapper> _mockMapper;


        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();

            _repositoryMock = new Mock<IDogRepository>();
            _policyStoreMock = new Mock<IIpPolicyStore>();
            _dogService = new DogService(_repositoryMock.Object, _policyStoreMock.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetDogsList_ShouldReturnListOfDogs()
        {
            // Arrange
            var expectedDogs = new List<Dog> { new Dog { Name = "Fido" }, new Dog { Name = "Rex" } };
            _repositoryMock.Setup(repo => repo.GetDogsList()).ReturnsAsync(expectedDogs);

            // Act
            var result = await _dogService.GetDogsList();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expectedDogs.Count));
        }

        [Test]
        public async Task SortedDogList_ShouldReturnSortedDogs()
        {
            // Arrange
            var inputDogs = new List<Dog>
            {
                new Dog { Name = "Mykhailo", Weight = 20, Tail_Lenght = 10 },
                new Dog { Name = "Rex", Weight = 15, Tail_Lenght = 12 },
                new Dog { Name = "Max", Weight = 25, Tail_Lenght = 8 }
            };

            _repositoryMock.Setup(repo => repo.GetDogsList()).ReturnsAsync(inputDogs);

            // Act
            var sortedDogs = await _dogService.SortedDogList("weight", "desc", 1, 2);

            // Assert
            Assert.IsNotNull(sortedDogs);
            Assert.That(sortedDogs.Count, Is.EqualTo(2));
            Assert.That(sortedDogs[0].Name, Is.EqualTo("Max")); 
            Assert.That(sortedDogs[1].Name, Is.EqualTo("Mykhailo"));
        }

        [Test]
        public async Task CreateDogAsync_ValidDto_ReturnsDogDto()
        {
            // Arrange
            var dogDto = new DogDto
            {
                Name = "Rover",
            };

            var expectedDog = new Dog
            {
                Name = "Rover",
            };

            _repositoryMock.Setup(repo => repo.CreateDog(It.IsAny<Dog>())).ReturnsAsync(expectedDog);
            _repositoryMock.Setup(repo => repo.GetDogsList())
                .ReturnsAsync(new List<Dog>());
            _mockMapper.Setup(mapper => mapper.Map<Dog>(It.IsAny<DogDto>())).Returns(expectedDog);
            _mockMapper.Setup(mapper => mapper.Map<DogDto>(It.IsAny<Dog>())).Returns(dogDto);

            // Act
            var result = await _dogService.CreateDogAsync(dogDto);

            // Assert
            _repositoryMock.Verify(repo => repo.CreateDog(expectedDog), Times.Once);
            _repositoryMock.Verify(repo => repo.GetDogsList(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<Dog>(dogDto), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<DogDto>(expectedDog), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DogDto>(result);
        }

    }
}
    