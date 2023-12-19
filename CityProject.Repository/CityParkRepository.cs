using AutoMapper;
using CityProject.DAL;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Repository
{
    public class CityParkRepository : ICityParkRepository
    {
        private readonly DataContext _dc;
        private readonly IMapper _mapper;

        public CityParkRepository(DataContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task AddAsync(CityPark cityPark)
        {
            var city = await _dc.Cities.FindAsync(cityPark.CityId);
            if (city == null)
                throw new Exception("City could not be found");
            await _dc.CityParks.AddAsync(cityPark);
        }

        public async Task DeleteAsync(int cityParkId)
        {
            var cityPark = await _dc.CityParks.FindAsync(cityParkId);
            if (cityPark == null)
                throw new Exception("Park could not be deleted");
            _dc.CityParks.Remove(cityPark);
        }

        public async Task<IEnumerable<CityPark>> GetAllAsync()
        {
            return await _dc.CityParks.ToListAsync();
        }

        public async Task<CityPark> GetAsync(int cityParkId)
        {
            var cityPark = await _dc.CityParks.FindAsync(cityParkId);
            if (cityPark == null)
                throw new Exception("Park could not be found");
            return cityPark;
        }

        public async Task<IEnumerable<CityPark>> GetParksByCityIdAsync(int cityId)
        {
            var city = await _dc.Cities.FindAsync(cityId);
            if (city == null)
                throw new Exception("City could not be found");
            return city.Parks;
        }

        public async Task UpdateAsync(UpdateCityParkDto cityParkDto, int cityParkId)
        {
            var cityPark = await _dc.CityParks.FindAsync(cityParkId);
            if (cityPark == null)
                throw new Exception("Park could not be found");
            _mapper.Map(cityParkDto, cityPark);
        }
    }
}
