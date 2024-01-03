using CityProject.Dtos;
using CityProject.Models;
using Sieve.Models;
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
        Task<List<CityDto>> GetAllCities(SieveModel sieveModel);

        Task AddCity(CityDto cityDto);
        Task DeleteCity(int id);

        Task UpdateCity(UpdateCityDto cityDto, int cityId);

    }
}
