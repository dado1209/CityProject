using AutoMapper;
using CityProject.Dtos;
using CityProject.Models;

namespace CityProject.Common
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<UpdateCityDto, City>();
            CreateMap<CityParkDto, CityPark>();
            CreateMap<CityPark, CityParkDto>();
            CreateMap<UpdateCityParkDto, CityPark>();
        }
    }
}
