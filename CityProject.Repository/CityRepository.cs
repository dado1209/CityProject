using CityProject.DAL;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Repository.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dc;
        private readonly IMapper _mapper;

        public CityRepository(DataContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }
        public async Task AddAsync(City city)
        {
            await _dc.Cities.AddAsync(city);
        }

        public async Task DeleteAsync(int cityId)
        {
            var city = await _dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be deleted");
            _dc.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _dc.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(int cityId)
        {
            var city = await _dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be found");
            return city;
        }

        public async Task UpdateAsync(UpdateCityDto cityDto, int cityId)
        {
            var city = await _dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be found");
            // Use auto_mapper to map new values from cityDto to city
            _mapper.Map(cityDto, city);
        }
    }
}

