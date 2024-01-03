using CityProject.DAL;
using CityProject.DAL.Entities;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityRepository : GenericRepository<CityEntity>, ICityRepository
    {
        public CityRepository(DataContext dc): base(dc) { }

    }
}

