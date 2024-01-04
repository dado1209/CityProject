using CityProject.Dtos;
using CityProject.Models;
using CityProject.Repository.Common;
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
        Task<City> GetCityById(int id);
        Task<List<City>> GetAllCities(SieveModel sieveModel);

        Task AddCity(City city);
        Task DeleteCity(int id);

        Task UpdateCity(UpdateCity city, int cityId);

    }
}
