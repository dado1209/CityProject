using ExampleProject.Dtos;
using ExampleProject.Models;

namespace ExampleProject.Repository.Common
{
    public interface ICityRepository : IGenericRepository<City, UpdateCityDto>
    {

    }
}
