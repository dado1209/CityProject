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
    public class CityParkServiceTests
    {
        private readonly CityParkService _cityParkService;
        private readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        // Use real mapper to test if result is cityDto
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfiles())));

        public CityParkServiceTests()
        {
            _cityParkService = new CityParkService(_uowMock.Object, mapper);
        }

        [Fact]
        public async Task AddCityPark_ShouldThrowException_WhenCityDoesNotExist()
        {
            // Arrange
            var cityParkDto = new CityParkDto { CityId = 1, Id = 1, Name = "drven park" };
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityParkDto.CityId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.AddCityPark(cityParkDto); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be found");
        }

        [Fact]
        public async Task AddCityPark_ShouldCompleteWithin100miliseconds_WhenCityDoesExist()
        {
            // Arrange
            var cityParkDto = new CityParkDto { CityId = 1, Id = 1, Name = "drven park" };
            var cityPark = new CityPark { CityId = cityParkDto.Id, Name = cityParkDto.Name, Id = cityParkDto.Id };
            var city = new City { Id = 1, Name = "osijek" };
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityParkDto.CityId)).ReturnsAsync(city);
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            _uowMock.Setup(x => x.CityParkRepository.AddAsync(cityPark)).Returns(Task.CompletedTask);
            // Act
            Func<Task> action = async () => { await _cityParkService.AddCityPark(cityParkDto); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }

        [Fact]
        public async Task DeleteCityPark_ShouldThrowException_WhenCityParkDoesNotExist()
        {   // Arrange
            int cityParkId = 1;
            _uowMock.Setup(x => x.CityParkRepository.GetAsync(cityParkId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.DeleteCityPark(cityParkId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("Park could not be found");
        }

        [Fact]
        public async Task DeleteCityPark_ShouldCompleteWithin100miliseconds_WhenCityParkDoesExist()
        {   // Arrange
            int cityParkId = 1;
            var cityPark = new CityPark { CityId = cityParkId, Name = "drven park", Id = 1};
            _uowMock.Setup(x => x.CityParkRepository.GetAsync(cityParkId)).ReturnsAsync(cityPark);
            // Use Verifiable() instead of Task.Completed when function is not async and returns void
            _uowMock.Setup(x => x.CityParkRepository.Delete(cityPark)).Verifiable();
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
            var cityPark1 = new CityPark { CityId = 1, Name = "drven park", Id = 1 };
            var cityPark2 = new CityPark { CityId = 1, Name = "vatren park", Id = 2 };
            var cityParks = new List<CityPark> { cityPark1, cityPark2 };
            _uowMock.Setup(x => x.CityParkRepository.GetAllAsync()).ReturnsAsync(cityParks);
            // Act
            var result = await _cityParkService.GetAllCityParks();
            // Assert
            result.Should().HaveCount(cityParks.Count());
            result.Should().BeOfType<List<CityParkDto>>();
        }

        [Fact]
        public async Task GetCityParkById_ShouldThrowException_WhenCityParkDoesNotExist()
        {
            // Arrange
            int cityParkId = 1;
            _uowMock.Setup(x => x.CityParkRepository.GetAsync(cityParkId)).ReturnsAsync(() => null);
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
            var cityPark = new CityPark { CityId = cityParkId, Name = "drven park", Id = 1 };
            var cityParkDto = new CityParkDto { CityId = cityParkId, Name = "drven park", Id = 1 };
            _uowMock.Setup(x => x.CityParkRepository.GetAsync(cityParkId)).ReturnsAsync(cityPark);
            // Act
            CityParkDto result = await _cityParkService.GetCityParkById(cityParkId);
            // Assert
            result.Should().BeOfType<CityParkDto>();
            result.Should().BeEquivalentTo(cityParkDto);
        }

        [Fact]
        public async Task GetCityParksByCityId_ShouldThrowException_WhenCityDoesNotExist()
        {   // Arrange
            int cityId = 1;
            _uowMock.Setup(x => x.CityRepository.GetAsync(cityId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.GetCityParksByCityId(cityId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("City could not be found");
        }

        [Fact]
        public async Task GetCityParksByCityId_ShouldReturnCollectionOfCityParkDtos_WhenCityDoesExist()
        {
            // Arrange
            var city = new City { Id = 1, Name = "osijek" };
            var cityPark1 = new CityPark { CityId = 1, Name = "drven park", Id = 1 };
            var cityPark2 = new CityPark { CityId = 1, Name = "vatren park", Id = 2 };
            var cityParks = new List<CityPark> { cityPark1, cityPark2 };
            var cityParkDto1 = new CityParkDto { CityId = 1, Name = "drven park", Id = 1 };
            var cityParkDto2 = new CityParkDto { CityId = 1, Name = "vatren park", Id = 2 };
            var cityParkDtos = new List<CityParkDto> { cityParkDto1, cityParkDto2 };
            _uowMock.Setup(x => x.CityRepository.GetAsync(city.Id)).ReturnsAsync(city);
            _uowMock.Setup(x => x.CityParkRepository.GetParksByCity(city)).Returns(cityParks);
            // Act
            var result = await _cityParkService.GetCityParksByCityId(city.Id);
            // Assert
            result.Should().HaveCount(cityParks.Count());
            result.Should().BeOfType<List<CityParkDto>>();
        }

        [Fact]
        public async Task UpdateCityPark_ShouldThrowException_WhenCityParkDoesNotExist()
        {   
            // Arrange
            int cityParkId = 1;
            var cityParkDto = new UpdateCityParkDto { CityId = cityParkId, Name = "drven park"};
            _uowMock.Setup(x => x.CityParkRepository.GetAsync(cityParkId)).ReturnsAsync(() => null);
            // Act
            Func<Task> action = async () => { await _cityParkService.UpdateCityPark(cityParkDto, cityParkId); };
            // Assert
            await action.Should().ThrowAsync<ObjectNotFoundException>().WithMessage("Park could not be updated");
        }

        [Fact]
        public async Task UpdateCityPark_ShouldCompleteWithin100miliseconds_WhenCityParkDoesExist()
        {   
            // Arrange
            int cityParkId = 1;
            var updateCityParkDto = new UpdateCityParkDto { CityId = cityParkId, Name = "drven park" };
            var cityPark = new CityPark { CityId = 1, Name = "vatren park", Id = 2 };
            _uowMock.Setup(x => x.CityParkRepository.GetAsync(cityParkId)).ReturnsAsync(cityPark);
            _uowMock.Setup(x => x.SaveAsync()).ReturnsAsync(true);
            // Act
            Func<Task> action = async () => { await _cityParkService.UpdateCityPark(updateCityParkDto, cityParkId); };
            // Assert
            await action.Should().CompleteWithinAsync(100.Milliseconds());
        }
    }


}
