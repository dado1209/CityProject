using AutoMapper;
using ExampleProject.DAL;
using ExampleProject.Dtos;
using ExampleProject.Models;
using ExampleProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace ExampleProject.Repository
{
    public class CityRepository :  ICityRepository
    {
        private readonly DataContext dc;
        private readonly IMapper mapper;

        public CityRepository(DataContext dc, IMapper mapper)
        {
            this.dc = dc;
            this.mapper = mapper;
        }
        public async Task AddAsync(City city)
        {
            await dc.Cities.AddAsync(city);
        }

        public async Task DeleteAsync(int cityId)
        {
            var city = await dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be deleted");
            dc.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await dc.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(int cityId)
        {
            var city = await dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be found");
            return city;
        }

        public async Task UpdateAsync(UpdateCityDto cityDto, int cityId)
        {
            var city = await dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be found");
            // Use automapper to map new values from cityDto to city
            mapper.Map(cityDto, city);
        }
    }
}
