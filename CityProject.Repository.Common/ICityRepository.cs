using CityProject.Dtos;
using CityProject.Models;

namespace CityProject.Repository.Common
{
    public interface ICityRepository : IGenericRepository<City, UpdateCityDto>
    {
    }
}
