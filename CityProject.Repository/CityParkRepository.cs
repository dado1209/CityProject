using CityProject.DAL;
using CityProject.DAL.Entities;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityParkRepository : GenericRepository<CityParkEntity>,ICityParkRepository
    {
        public CityParkRepository(DataContext dc) : base(dc) { }

        public IQueryable<CityParkEntity> GetParksByCity(CityEntity city)
        {
            return city.Parks.AsQueryable();
        }
    }
}
