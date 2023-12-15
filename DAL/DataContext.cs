using ExampleProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleProject.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }

        public DbSet<City> Cities
        { get; set; }

        public DbSet<CityPark> CityParks { get; set; }
    }
}
