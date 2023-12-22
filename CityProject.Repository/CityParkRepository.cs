using CityProject.DAL;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityParkRepository : ICityParkRepository
    {
        private readonly DataContext _dc;

        public CityParkRepository(DataContext dc)
        {
            _dc = dc;
        }

        public async Task AddAsync(CityPark cityPark)
        {
            await _dc.CityParks.AddAsync(cityPark);
        }

        public void Delete(CityPark cityPark)
        {
            _dc.CityParks.Remove(cityPark);
        }

        public async Task<IEnumerable<CityPark>> GetAllAsync()
        {
            return await _dc.CityParks.ToListAsync();
        }

        public async Task<CityPark> GetAsync(int cityParkId)
        {
            return await _dc.CityParks.FindAsync(cityParkId); ;
        }

        public IEnumerable<CityPark> GetParksByCity(City city)
        {
            return city.Parks;
        }
    }
}
