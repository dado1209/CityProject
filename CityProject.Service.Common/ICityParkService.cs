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
        Task<CityParkDto> GetCityParkById(int cityParkId);
        Task<List<CityParkDto>> GetAllCityParks(SieveModel sieveModel);

        Task AddCityPark(CityParkDto cityParkDto);
        Task DeleteCityPark(int cityParkId);

        Task UpdateCityPark(UpdateCityParkDto cityParkDto, int cityParkId);

        Task<IEnumerable<CityParkDto>> GetCityParksByCityId(int cityId);
    }
}
