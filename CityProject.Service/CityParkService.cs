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

        public CityParkService(IUnitOfWork uow, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _uow = uow;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
        public async Task AddCityPark(CityPark cityPark)
        {
            // Map the cityPark to cityParkEntity
            var cityParkEntity = _mapper.Map<CityParkEntity>(cityPark);
            var cityEntity = await _uow.CityRepository.GetAsync(cityParkEntity.CityId);
            if (cityEntity == null) throw new ObjectNotFoundException("City could not be found");
            await _uow.CityParkRepository.AddAsync(cityParkEntity);
            await _uow.SaveAsync();
        }

        public async Task DeleteCityPark(int cityParkId)
        {
            var cityParkEntity = await _uow.CityParkRepository.GetAsync(cityParkId);
            if (cityParkEntity == null) throw new ObjectNotFoundException("Park could not be found");
            _uow.CityParkRepository.Delete(cityParkEntity);
            await _uow.SaveAsync();
        }

        public async Task<List<CityPark>> GetAllCityParks(SieveModel sieveModel)
        {
            //apply query parameters and get all city parks which match
            var cityParkEntities = _sieveProcessor.Apply(sieveModel, _uow.CityParkRepository.GetAllAsync());
            // Map city parks to city park dtos before returning the value
            return _mapper.Map<List<CityPark>>(cityParkEntities);
        }

        public async Task<CityPark> GetCityParkById(int cityParkId)
        {
            var cityParkEntity = await _uow.CityParkRepository.GetAsync(cityParkId);
            if (cityParkEntity == null) throw new ObjectNotFoundException("Park could not be found");
            return _mapper.Map<CityPark>(cityParkEntity);
        }

        public async Task<List<CityPark>> GetCityParksByCityId(SieveModel sieveModel,int cityId)
        {
            var cityEntity = await _uow.CityRepository.GetAsync(cityId);
            if (cityEntity == null) throw new ObjectNotFoundException("City could not be found");
            var cityParkEntities = _sieveProcessor.Apply(sieveModel, _uow.CityParkRepository.GetParksByCity(cityEntity));
            return _mapper.Map<List<CityPark>>(cityParkEntities);
        }

        public async Task UpdateCityPark(UpdateCityPark cityPark, int cityParkId)
        {
            var cityParkEntity = await _uow.CityParkRepository.GetAsync(cityParkId);
            if (cityParkEntity == null) throw new ObjectNotFoundException("Park could not be updated");
            // Use auto_mapper to map new values from cityPark to cityParkEntity
            _mapper.Map(cityPark, cityParkEntity);
            await _uow.SaveAsync();
        }
    }
}
