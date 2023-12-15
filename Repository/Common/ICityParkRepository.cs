using ExampleProject.Dtos;
using ExampleProject.Models;

namespace ExampleProject.Repository.Common
{
    public interface ICityParkRepository : IGenericRepository<CityPark, UpdateCityParkDto>
    {
        Task<IEnumerable<CityPark>> GetParksByCityIdAsync(int id);
    }
}
