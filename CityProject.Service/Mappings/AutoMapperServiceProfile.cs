using AutoMapper;
using CityProject.DAL.Entities;
using CityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Service.Mappings
{
    // All mapping related to the service layer
    public class AutoMapperServiceProfile : Profile
    {
        public AutoMapperServiceProfile() {
            CreateMap<City, CityEntity>();
            CreateMap<CityEntity, City>();
            CreateMap<UpdateCity, CityEntity>();
            CreateMap<CityPark, CityParkEntity>();
            CreateMap<CityParkEntity, CityPark>();
            CreateMap<UpdateCityPark, CityParkEntity>();
        }
    }
}
