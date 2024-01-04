using CityProject.Dtos;
using CityProject.Models;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Service.Common
{
    public interface ICityParkService
    {
        Task<CityPark> GetCityParkById(int cityParkId);
        Task<List<CityPark>> GetAllCityParks(SieveModel sieveModel);

        Task AddCityPark(CityPark cityPark);
        Task DeleteCityPark(int cityParkId);

        Task UpdateCityPark(UpdateCityPark cityPark, int cityParkId);

        Task<List<CityPark>> GetCityParksByCityId(SieveModel sieveModel,int cityId);
    }
}
