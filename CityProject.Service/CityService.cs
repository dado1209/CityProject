using CityProject.Repository.Common;
using CityProject.Dtos;
using CityProject.Models;
using AutoMapper;
using System.Data.Entity.Core;
using Sieve.Models;
using Sieve.Services;
using CityProject.DAL.Entities;

namespace CityProject.Service
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        private ICityRepository _cityRepository { get; }

        public CityService(IUnitOfWork uow, IMapper mapper, ISieveProcessor sieveProcessor, ICityRepository cityRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _cityRepository = cityRepository;
        }
        public async Task AddCity(City city)
        {
            // Map city to cityEntity
            var cityEntity = _mapper.Map<CityEntity>(city);
            await _cityRepository.AddAsync(cityEntity);
            await _uow.SaveAsync();
        }

        public async Task DeleteCity(int cityId)
        {
            var city = await _cityRepository.GetAsync(cityId);
            if (city == null) throw new ObjectNotFoundException("City could not be deleted");
            _cityRepository.Delete(city);
            await _uow.SaveAsync();
        }

        public async Task<List<City>> GetAllCities(SieveModel sieveModel)
        {
            //apply query parameters and get all city entities which match
            var cityEntities = _sieveProcessor.Apply(sieveModel, _cityRepository.GetAll());
            // Map city entities to cities before returning the value
            return _mapper.Map<List<City>>(cityEntities); 
        }

        public async Task<City> GetCityById(int cityId)
        {
            var cityEntity = await _cityRepository.GetAsync(cityId);
            if (cityEntity == null) throw new ObjectNotFoundException("City could not be found");
            return _mapper.Map<City>(cityEntity);
        }

        public async Task UpdateCity(UpdateCity city, int cityId)
        {
            var cityEntity = await _cityRepository.GetAsync(cityId);
            if (cityEntity == null) throw new ObjectNotFoundException("City could not be updated");
            // Use auto_mapper to map new values from UpdateCity to cityEntity
            _mapper.Map(city, cityEntity);
            await _uow.SaveAsync();
        }
    }
}
