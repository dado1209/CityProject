using CityProject.DAL;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityParkRepository : GenericRepository<CityPark>,ICityParkRepository
    {
        public CityParkRepository(DataContext dc) : base(dc) { }

        public IEnumerable<CityPark> GetParksByCity(City city)
        {
            return city.Parks;
        }
    }
}
