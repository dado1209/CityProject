using CityProject.DAL;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dc;
  

        public CityRepository(DataContext dc)
        {
            _dc = dc;
        }
        public async Task AddAsync(City city)
        {
            await _dc.Cities.AddAsync(city);
        }

        public void Delete(City city)
        {
            _dc.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _dc.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(int cityId)
        {
            return await _dc.Cities.FindAsync(cityId);
        }

    }
}

