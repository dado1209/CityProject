using CityProject.Dtos;
using CityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Service
{
    public interface ICityService
    {
        Task<CityDto> GetCityById(int id);
        Task<IEnumerable<CityDto>> GetAllCities();

        Task AddCity(CityDto cityDto);
        Task DeleteCity(int id);

        Task UpdateCity(UpdateCityDto cityDto, int cityId);

    }
}
