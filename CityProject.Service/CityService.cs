﻿using CityProject.DAL.Common;
using CityProject.Dtos;
using CityProject.Models;
using AutoMapper;
using System.Data.Entity.Core;

namespace CityProject.Service
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task AddCity(CityDto cityDto)
        {
            // Map cityDto to city
            var city = _mapper.Map<City>(cityDto);
            await _uow.CityRepository.AddAsync(city);
            await _uow.SaveAsync();
        }

        public async Task DeleteCity(int cityId)
        {
            var city = await _uow.CityRepository.GetAsync(cityId);
            if (city == null) throw new ObjectNotFoundException("City could not be deleted");
            _uow.CityRepository.Delete(city);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<CityDto>> GetAllCities()
        {
            var cities = await _uow.CityRepository.GetAllAsync();
            // Map cities to cities dto and return before returning the value
            return  _mapper.Map<IEnumerable<CityDto>>(cities); ;
        }

        public async Task<CityDto> GetCityById(int cityId)
        {
            var city = await _uow.CityRepository.GetAsync(cityId);
            if (city == null) throw new ObjectNotFoundException("City could not be found");
            return _mapper.Map<CityDto>(city);
        }

        public async Task UpdateCity(UpdateCityDto cityDto, int cityId)
        {
            var city = await _uow.CityRepository.GetAsync(cityId);
            if (city == null) throw new ObjectNotFoundException("City could not be updated");
            // Use auto_mapper to map new values from cityDto to city
            _mapper.Map(cityDto, city);
            await _uow.SaveAsync();
        }
    }
}