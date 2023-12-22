using AutoMapper;
using CityProject.DAL.Common;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service.Common;
using System.Data.Entity.Core;

namespace CityProject.Service
{
    public class CityParkService : ICityParkService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityParkService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task AddCityPark(CityParkDto cityParkDto)
        {
            // Map the cityParkDto to cityPark
            var cityPark = _mapper.Map<CityPark>(cityParkDto);
            var city = await _uow.CityRepository.GetAsync(cityPark.CityId);
            if (city == null) throw new ObjectNotFoundException("City could not be found");
            await _uow.CityParkRepository.AddAsync(cityPark);
            await _uow.SaveAsync();
        }

        public async Task DeleteCityPark(int cityParkId)
        {
            var cityPark = await _uow.CityParkRepository.GetAsync(cityParkId);
            if (cityPark == null) throw new ObjectNotFoundException("Park could not be found");
            _uow.CityParkRepository.Delete(cityPark);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<CityParkDto>> GetAllCityParks()
        {
            var cityParks = await _uow.CityParkRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CityParkDto>>(cityParks);
        }

        public async Task<CityParkDto> GetCityParkById(int cityParkId)
        {
            var cityPark = await _uow.CityParkRepository.GetAsync(cityParkId);
            if (cityPark == null) throw new ObjectNotFoundException("Park could not be found");
            return _mapper.Map<CityParkDto>(cityPark);
        }

        public async Task<IEnumerable<CityParkDto>> GetCityParksByCityId(int cityId)
        {
            var city = await _uow.CityRepository.GetAsync(cityId);
            if (city == null) throw new ObjectNotFoundException("City could not be found");
            var cityParks = _uow.CityParkRepository.GetParksByCity(city);
            return _mapper.Map<IEnumerable<CityParkDto>>(cityParks);
        }

        public async Task UpdateCityPark(UpdateCityParkDto cityParkDto, int cityParkId)
        {
            var cityPark = await _uow.CityParkRepository.GetAsync(cityParkId);
            if (cityPark == null) throw new ObjectNotFoundException("Park could not be updated");
            // Use auto_mapper to map new values from cityParkDto to cityPark
            _mapper.Map(cityParkDto, cityParkId);
            await _uow.SaveAsync();
        }
    }
}
