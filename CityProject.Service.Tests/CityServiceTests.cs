using AutoMapper;
using CityProject.Common;
using CityProject.DAL.Common;
using CityProject.Dtos;
using CityProject.Models;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Core;

namespace CityProject.Service.Tests
{
    public class CityServiceTests
    {
        private readonly CityService _cityService;
        private readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        // Use real mapper to test if result is cityDto
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfiles())));

        public CityServiceTests() {
            _cityService = new CityService(_uowMock.Object, mapper);
        }

        [Fact]
        public async Task GetCityById_ShouldReturnCityDto_WhenCityExists()
        {
            // Arrange
            var cityId = 1;
            var cityName = "osijek";
            var city = new City { Id = cityId, Name = cityName };
            var cityDto = new CityDto { Id = cityId, Name = cityName };
            //var cityDto = new CityDto { Id = cityId, Name = cityName };
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(city);
            //_mapperMock.Setup(x => x.Map<CityDto>(city)).Returns(cityDto);

            // Act
            CityDto result = await _cityService.GetCityById(cityId);
            // Assert
            result.Should().BeOfType<CityDto>();
            result.Should().BeEquivalentTo(cityDto);
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
            var city1 = new City { Id = 1, Name = "osijek" };
            var city2 = new City { Id = 2, Name = "zagreb" };
            var cities = new List<City> { city1, city2 };

            _uowMock.Setup(x => x.CityRepository.GetAllAsync()).ReturnsAsync(cities);
            // Act
            var result = await _cityService.GetAllCities();
            // Assert
            result.Should().HaveCount(cities.Count());
            result.Should().BeOfType<List<CityDto>>();
        }

        [Fact]
        public async Task AddCity_ShouldCompleteWithin100miliseconds()
        {
            // Arrange
            var cityDto = new CityDto { Id = 1, Name = "osijek" };
            var city = new City { Id = 1, Name = "osijek" };
            _uowMock.Setup(x => x.CityRepository.AddAsync(city)).Returns(Task.CompletedTask);
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act 
            Func<Task> action = async () => { await _cityService.AddCity(cityDto); };

            // Assert 
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }

        [Fact]
        public async Task DeleteCity_ShouldCompleteWithin100miliseconds_WhenCityExists()
        {
            // Arrange
            var city = new City { Id = 1, Name = "osijek" };
            _uowMock.Setup(x => x.CityRepository.GetAsync(city.Id)).ReturnsAsync(city);
            // Use Verifiable() instead of Task.Completed when function is not async and returns void
            _uowMock.Setup(x => x.CityRepository.Delete(city)).Verifiable();
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act
            Func<Task> action = async () => { await _cityService.DeleteCity(city.Id); };

            // Assert 
            await action.Should().CompleteWithinAsync(100.Milliseconds());

        }

        [Fact]
        public async Task UpdateCity_ShouldThrowException_WhenCityDoesNotExist()
        {
            // Arrange
            var updateCityDto = new UpdateCityDto { Name = "osijek" };
            var cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityService.UpdateCity(updateCityDto ,cityId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be updated");
        }

        [Fact]
        public async Task UpdateCity_ShouldCompleteWithin100miliseconds_WhenCityExists()
        {
            // Arrange
            var city = new City { Id = 1, Name = "zagreb" };
            var updateCityDto = new UpdateCityDto { Name = "osijek" };
            var cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(city);
            // Act

            Func<Task> action = async () => { await _cityService.UpdateCity(updateCityDto, cityId); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }
    }
}