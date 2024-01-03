using AutoMapper;
using CityProject.DAL.Entities;
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
            CreateMap<UpdateCityDto, UpdateCity>();
            CreateMap<CityParkDto, CityPark>();
            CreateMap<CityPark, CityParkDto>();
            CreateMap<UpdateCityParkDto, UpdateCityPark>();


            CreateMap<City, CityEntity>();
            CreateMap<CityEntity, City>();
            CreateMap<UpdateCity, CityEntity>();
            CreateMap<CityPark, CityParkEntity>();
            CreateMap<CityParkEntity, CityPark>();
            CreateMap<UpdateCityPark, CityParkEntity>();
        }
    }
}
