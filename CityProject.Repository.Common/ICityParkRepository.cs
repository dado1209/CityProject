using CityProject.Dtos;
using CityProject.Models;


namespace CityProject.Repository.Common
{
    public interface ICityParkRepository : IGenericRepository<CityPark, UpdateCityParkDto>
    {
        Task<IEnumerable<CityPark>> GetParksByCityIdAsync(int id);
    }
}
