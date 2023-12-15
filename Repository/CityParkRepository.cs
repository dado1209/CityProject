using AutoMapper;
using ExampleProject.DAL;
using ExampleProject.Dtos;
using ExampleProject.Models;
using ExampleProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace ExampleProject.Repository
{
    public class CityParkRepository : ICityParkRepository
    {
        private readonly DataContext dc;
        private readonly IMapper mapper;

        public CityParkRepository(DataContext dc, IMapper mapper)
        {
            this.dc = dc;
            this.mapper = mapper;
        }

        public async Task AddAsync(CityPark cityPark)
        {
            await dc.CityParks.AddAsync(cityPark);
        }

        public async Task DeleteAsync(int cityParkId)
        {
            var cityPark = await dc.CityParks.FindAsync(cityParkId);
            if (cityPark == null)
                throw new Exception("Park could not be deleted");
            dc.CityParks.Remove(cityPark);
        }

        public async Task<IEnumerable<CityPark>> GetAllAsync()
        {
            return await dc.CityParks.ToListAsync();
        }

        public async Task<CityPark> GetAsync(int cityParkId)
        {
            var cityPark = await dc.CityParks.FindAsync(cityParkId);
            if (cityPark == null)
                throw new Exception("Park could not be found");
            return cityPark;
        }

        public async Task<IEnumerable<CityPark>> GetParksByCityIdAsync(int cityId)
        {
            var city = await dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be found");
            return city.Parks;
        }

        public async Task UpdateAsync(UpdateCityParkDto cityParkDto, int cityParkId)
        {
            var cityPark = await dc.CityParks.FindAsync(cityParkId);
            if (cityPark == null)
                throw new Exception("Park could not be found");
            mapper.Map(cityParkDto, cityPark);
        }
    }
}
