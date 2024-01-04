using AutoMapper;
using CityProject.Repository.Common;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service.Common;
using System.Data.Entity.Core;
using Sieve.Services;
using Sieve.Models;
using CityProject.DAL.Entities;

namespace CityProject.Service
{
    public class CityParkService : ICityParkService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        private ICityRepository _cityRepository { get; }
        private ICityParkRepository _cityParkRepository { get; }

        public CityParkService(IUnitOfWork uow, IMapper mapper, ISieveProcessor sieveProcessor, ICityRepository cityRepository, ICityParkRepository cityParkRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _cityRepository = cityRepository;
            _cityParkRepository = cityParkRepository;
        }
        public async Task AddCityPark(CityPark cityPark)
        {
            // Map the cityPark to cityParkEntity
            var cityParkEntity = _mapper.Map<CityParkEntity>(cityPark);
            var cityEntity = await _cityRepository.GetAsync(cityParkEntity.CityId);
            if (cityEntity == null) throw new ObjectNotFoundException("City could not be found");
            await _cityParkRepository.AddAsync(cityParkEntity);
            await _uow.SaveAsync();
        }

        public async Task DeleteCityPark(int cityParkId)
        {
            var cityParkEntity = await _cityParkRepository.GetAsync(cityParkId);
            if (cityParkEntity == null) throw new ObjectNotFoundException("Park could not be found");
            _cityParkRepository.Delete(cityParkEntity);
            await _uow.SaveAsync();
        }

        public async Task<List<CityPark>> GetAllCityParks(SieveModel sieveModel)
        {
            //apply query parameters and get all city parks which match
            var cityParkEntities = _sieveProcessor.Apply(sieveModel, _cityParkRepository.GetAll());
            // Map city parks to city park dtos before returning the value
            return _mapper.Map<List<CityPark>>(cityParkEntities);
        }

        public async Task<CityPark> GetCityParkById(int cityParkId)
        {
            var cityParkEntity = await _cityParkRepository.GetAsync(cityParkId);
            if (cityParkEntity == null) throw new ObjectNotFoundException("Park could not be found");
            return _mapper.Map<CityPark>(cityParkEntity);
        }

        public async Task<List<CityPark>> GetCityParksByCityId(SieveModel sieveModel,int cityId)
        {
            var cityEntity = await _cityRepository.GetAsync(cityId);
            if (cityEntity == null) throw new ObjectNotFoundException("City could not be found");
            var cityParkEntities = _sieveProcessor.Apply(sieveModel, _cityParkRepository.GetParksByCity(cityEntity));
            return _mapper.Map<List<CityPark>>(cityParkEntities);
        }

        public async Task UpdateCityPark(UpdateCityPark cityPark, int cityParkId)
        {
            var cityParkEntity = await _cityParkRepository.GetAsync(cityParkId);
            if (cityParkEntity == null) throw new ObjectNotFoundException("Park could not be updated");
            // Use auto_mapper to map new values from cityPark to cityParkEntity
            _mapper.Map(cityPark, cityParkEntity);
            await _uow.SaveAsync();
        }
    }
}
