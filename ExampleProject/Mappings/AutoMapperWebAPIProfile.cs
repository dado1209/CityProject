using AutoMapper;
using CityProject.Dtos;
using CityProject.Models;

namespace CityProject.WebAPI.Mappings
{
    // All mapping related to the WebAPI layer
    public class AutoMapperWebAPIProfile: Profile
    {
        public AutoMapperWebAPIProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<UpdateCityDto, UpdateCity>();
            CreateMap<CityParkDto, CityPark>();
            CreateMap<CityPark, CityParkDto>();
            CreateMap<UpdateCityParkDto, UpdateCityPark>();
        }
    }
}
