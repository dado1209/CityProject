using CityProject.DAL;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CityProject.Repository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(DataContext dc): base(dc) { }

    }
}

