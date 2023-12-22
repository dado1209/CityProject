using CityProject.Dtos;
using CityProject.Models;


namespace CityProject.Repository.Common
{
    public interface ICityParkRepository : IGenericRepository<CityPark>
    {
        IEnumerable<CityPark> GetParksByCity(City city);
    }
}
