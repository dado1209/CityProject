using AutoMapper;
using CityProject.Common;
using CityProject.Repository.Common;
using CityProject.Dtos;
using CityProject.Models;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Core;
using Sieve.Models;
using Sieve.Services;
using CityProject.DAL.Entities;

namespace CityProject.Service.Tests
{
    public class CityServiceTests
    {
        private readonly CityService _cityService;
        private readonly Mock<IUnitOfWork> _uowMock = new();
        // Use real mapper to test if result is cityDto
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfiles())));
        private static readonly ISieveProcessor _sieveProcessor = null;

        public CityServiceTests() {
            _cityService = new CityService(_uowMock.Object, mapper, _sieveProcessor);
        }

        [Fact]
        public async Task GetCityById_ShouldReturnCityDto_WhenCityExists()
        {
            // Arrange
            var cityId = 1;
            var cityName = "osijek";
            var cityEntity = new CityEntity { Id = cityId, Name = cityName };
            var city = new City { Id = cityId, Name = cityName };
            //var cityDto = new CityDto { Id = cityId, Name = cityName };
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(cityEntity);
            //_mapperMock.Setup(x => x.Map<CityDto>(city)).Returns(cityDto);

            // Act
            City result = await _cityService.GetCityById(cityId);
            // Assert
            result.Should().BeOfType<City>();
            result.Should().BeEquivalentTo(city);
        }

        [Fact]
        public async Task GetCityById_ShouldThrowException_WhenCityDoesNotExist()
        {
            // Arrange
            var cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(() => null);

            // Act 
            Func<Task> action = async () => { var result = await _cityService.GetCityById(cityId); };

            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be found");
        }

        [Fact]
        public async Task DeleteCity_ShouldThrowException_WhenCityDoesNotExist()
        {
            // Arrange
            var cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(() => null);

            // Act 
            Func<Task> action = async () => { await _cityService.DeleteCity(cityId); };

            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be deleted");
        }

        [Fact]
        public async Task GetAllCities_ShouldReturnCollectionOfCityDtos()
        {   
            // Arrange
            var cityEntity1 = new CityEntity { Id = 1, Name = "osijek" };
            var cityEntity2 = new CityEntity { Id = 2, Name = "zagreb" };
            var cityEntities = new List<CityEntity> { cityEntity1, cityEntity2 }.AsQueryable();
            var sieveModel = new SieveModel();

            _uowMock.Setup(x => x.CityRepository.GetAllAsync()).Returns(cityEntities);
            // Act
            var result = await _cityService.GetAllCities(sieveModel);
            // Assert
            result.Should().BeOfType<List<City>>();
        }

        [Fact]
        public async Task AddCity_ShouldCompleteWithin100miliseconds()
        {
            // Arrange
            var city = new City { Id = 1, Name = "osijek" };
            var cityEntity = new CityEntity { Id = 1, Name = "osijek" };
            _uowMock.Setup(x => x.CityRepository.AddAsync(cityEntity)).Returns(Task.CompletedTask);
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act 
            Func<Task> action = async () => { await _cityService.AddCity(city); };

            // Assert 
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }

        [Fact]
        public async Task DeleteCity_ShouldCompleteWithin100miliseconds_WhenCityExists()
        {
            // Arrange
            var cityEntity = new CityEntity { Id = 1, Name = "osijek" };
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityEntity.Id)).ReturnsAsync(cityEntity);
            // Use Verifiable() instead of Task.Completed when function is not async and returns void
            _uowMock.Setup(x => x.CityRepository.Delete(cityEntity)).Verifiable();
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act
            Func<Task> action = async () => { await _cityService.DeleteCity(cityEntity.Id); };

            // Assert 
            await action.Should().CompleteWithinAsync(100.Milliseconds());

        }

        [Fact]
        public async Task UpdateCity_ShouldThrowException_WhenCityDoesNotExist()
        {
            // Arrange
            var updateCity = new UpdateCity { Name = "osijek" };
            var cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityService.UpdateCity(updateCity, cityId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be updated");
        }

        [Fact]
        public async Task UpdateCity_ShouldCompleteWithin100miliseconds_WhenCityExists()
        {
            // Arrange
            var cityEntity = new CityEntity { Id = 1, Name = "zagreb" };
            var updateCity = new UpdateCity { Name = "osijek" };
            var cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(cityEntity);
            // Act

            Func<Task> action = async () => { await _cityService.UpdateCity(updateCity, cityId); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }
    }
}