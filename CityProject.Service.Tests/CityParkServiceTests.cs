using AutoMapper;
using CityProject.Repository.Common;
using CityProject.Dtos;
using CityProject.Models;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Core;
using Sieve.Services;
using CityProject.DAL.Entities;
using Sieve.Models;
using CityProject.Service.Mappings;


namespace CityProject.Service.Tests
{
    public class CityParkServiceTests
    {
        private readonly CityParkService _cityParkService;
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<ICityRepository> _cityRepositoryMock = new();
        private readonly Mock<ICityParkRepository> _cityParkRepositoryMock = new();
        // Use real mapper to test if result is cityDto
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperServiceProfile())));
        private static readonly Mock<ISieveProcessor> _sieveProcessorMock = new();

        public CityParkServiceTests()
        {
            _cityParkService = new CityParkService(_uowMock.Object, mapper, _sieveProcessorMock.Object, _cityRepositoryMock.Object, _cityParkRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCityPark_ShouldThrowException_WhenCityDoesNotExist()
        {
            // Arrange
            var cityPark = new CityPark { CityId = 1, Id = 1, Name = "drven park" };
            _cityRepositoryMock.Setup(x => x.GetAsync(cityPark.CityId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.AddCityPark(cityPark); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be found");
        }

        [Fact]
        public async Task AddCityPark_ShouldCompleteWithin100miliseconds_WhenCityDoesExist()
        {
            // Arrange
            var cityPark = new CityPark { CityId = 1, Id = 1, Name = "drven park" };
            var cityParkEntity = new CityParkEntity { CityId = cityPark.Id, Name = cityPark.Name, Id = cityPark.Id };
            var cityEntity = new CityEntity { Id = 1, Name = "osijek" };
            _cityRepositoryMock.Setup(x => x.GetAsync(cityPark.CityId)).ReturnsAsync(cityEntity);
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            _cityParkRepositoryMock.Setup(x => x.AddAsync(cityParkEntity)).Returns(Task.CompletedTask);
            // Act
            Func<Task> action = async () => { await _cityParkService.AddCityPark(cityPark); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }

        [Fact]
        public async Task DeleteCityPark_ShouldThrowException_WhenCityParkDoesNotExist()
        {   // Arrange
            int cityParkId = 1;
            _cityParkRepositoryMock.Setup(x => x.GetAsync(cityParkId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.DeleteCityPark(cityParkId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("Park could not be found");
        }

        [Fact]
        public async Task DeleteCityPark_ShouldCompleteWithin100miliseconds_WhenCityParkDoesExist()
        {   // Arrange
            int cityParkId = 1;
            var cityParkEntity = new CityParkEntity { CityId = cityParkId, Name = "drven park", Id = 1};
            _cityParkRepositoryMock.Setup(x => x.GetAsync(cityParkId)).ReturnsAsync(cityParkEntity);
            // Use Verifiable() instead of Task.Completed when function is not async and returns void
            _cityParkRepositoryMock.Setup(x => x.Delete(cityParkEntity)).Verifiable();
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act
            Func<Task> action = async () => { await _cityParkService.DeleteCityPark(cityParkId); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }

        [Fact]
        public async Task GetAllCityParks_ShouldReturnCollectionOfCityParkDtos()
        {   
            // Arrange
            var cityParkEntity1 = new CityParkEntity { CityId = 1, Name = "drven park", Id = 1 };
            var cityParkEntity2 = new CityParkEntity { CityId = 1, Name = "vatren park", Id = 2 };
            var cityParkEntities = new List<CityParkEntity> { cityParkEntity1, cityParkEntity2 }.AsQueryable();
            var sieveModel = new SieveModel();
            _cityParkRepositoryMock.Setup(x => x.GetAll()).Returns(cityParkEntities);
            _sieveProcessorMock.Setup(x => x.Apply(sieveModel, _cityParkRepositoryMock.Object.GetAll(), null, false, false, false)).Returns(cityParkEntities);
            // Act
            var result = await _cityParkService.GetAllCityParks(sieveModel);
            // Assert
            result.Should().BeOfType<List<CityPark>>();
        }

        [Fact]
        public async Task GetCityParkById_ShouldThrowException_WhenCityParkDoesNotExist()
        {
            // Arrange
            int cityParkId = 1;
            _cityParkRepositoryMock.Setup(x => x.GetAsync(cityParkId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.GetCityParkById(cityParkId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("Park could not be found");
        }

        [Fact]
        public async Task GetCityParkById_ShouldReturnCityParkDto_WhenCityParkDoesExist()
        {   
            // Arrange
            int cityParkId = 1;
            var cityParkEntity = new CityParkEntity { CityId = cityParkId, Name = "drven park", Id = 1 };
            var cityPark = new CityPark { CityId = cityParkId, Name = "drven park", Id = 1 };
            _cityParkRepositoryMock.Setup(x => x.GetAsync(cityParkId)).ReturnsAsync(cityParkEntity);
            // Act
            CityPark result = await _cityParkService.GetCityParkById(cityParkId);
            // Assert
            result.Should().BeOfType<CityPark>();
            result.Should().BeEquivalentTo(cityPark);
        }

        [Fact]
        public async Task GetCityParksByCityId_ShouldThrowException_WhenCityDoesNotExist()
        {   // Arrange
            var sieveModel = new SieveModel();
            int cityId = 1;
            _cityRepositoryMock.Setup(x => x.GetAsync(cityId)).ReturnsAsync(() => null);

            // Act
            Func<Task> action = async () => { await _cityParkService.GetCityParksByCityId(sieveModel,cityId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be found");
        }

        [Fact]
        public async Task GetCityParksByCityId_ShouldReturnCollectionOfCityParkDtos_WhenCityDoesExist()
        {
            // Arrange
            var cityEntity = new CityEntity { Id = 1, Name = "osijek" };
            var cityParkEntity1 = new CityParkEntity { CityId = 1, Name = "drven park", Id = 1 };
            var cityParkEntity2 = new CityParkEntity { CityId = 1, Name = "vatren park", Id = 2 };
            var cityParkEntities = new List<CityParkEntity> { cityParkEntity1, cityParkEntity2 };
            var cityPark1 = new CityPark { CityId = 1, Name = "drven park", Id = 1 };
            var cityPark2 = new CityPark { CityId = 1, Name = "vatren park", Id = 2 };
            var cityParks = new List<CityPark> { cityPark1, cityPark2 };
            var sieveModel = new SieveModel();
            _cityRepositoryMock.Setup(x => x.GetAsync(cityEntity.Id)).ReturnsAsync(cityEntity);
            _cityParkRepositoryMock.Setup(x => x.GetParksByCity(cityEntity)).Returns(cityParkEntities.AsQueryable());
            // Act
            var result = await _cityParkService.GetCityParksByCityId(sieveModel,cityEntity.Id);
            // Assert
            result.Should().BeOfType<List<CityPark>>();
        }

        [Fact]
        public async Task UpdateCityPark_ShouldThrowException_WhenCityParkDoesNotExist()
        {   
            // Arrange
            int cityParkId = 1;
            var cityPark = new UpdateCityPark { CityId = cityParkId, Name = "drven park"};
            _cityParkRepositoryMock.Setup(x => x.GetAsync(cityParkId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.UpdateCityPark(cityPark, cityParkId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("Park could not be updated");
        }

        [Fact]
        public async Task UpdateCityPark_ShouldCompleteWithin100miliseconds_WhenCityParkDoesExist()
        {   
            // Arrange
            int cityParkId = 1;
            var updateCityPark = new UpdateCityPark { CityId = cityParkId, Name = "drven park" };
            var cityParkEntity = new CityParkEntity { CityId = 1, Name = "vatren park", Id = 2 };
            _cityParkRepositoryMock.Setup(x => x.GetAsync(cityParkId)).ReturnsAsync(cityParkEntity);
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act
            Func<Task> action = async () => { await _cityParkService.UpdateCityPark(updateCityPark, cityParkId); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }
    }


}
