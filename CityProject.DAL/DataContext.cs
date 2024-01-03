using CityProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityProject.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CityEntity> Cities
        { get; set; }

        public DbSet<CityParkEntity> CityParks { get; set; }
    }
}
