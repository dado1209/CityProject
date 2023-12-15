using AutoMapper;
using ExampleProject.Dtos;
using ExampleProject.Models;

namespace ExampleProject.Common
{
    public class AutoMapperProfiles : Profile
    {
       public AutoMapperProfiles() {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<UpdateCityDto, City>();
            CreateMap<CityParkDto, CityPark>();
            CreateMap<CityPark, CityParkDto>();
        }
    }
}
