using AutoMapper;
using CityProject.DAL.Common;
using CityProject.Dtos;
using CityProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        // GET api/City
        public async Task<IActionResult> GetCities() { 
            var cities = await _uow.CityRepository.GetAllAsync();
            var citiesDto = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }
        [HttpPost]
        // POST api/City
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _uow.CityRepository.AddAsync(city);
            await _uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        // DELETE api/City/{id}
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _uow.CityRepository.DeleteAsync(id);
            await _uow.SaveAsync();
            return Ok(id);
        }

        [HttpPut("{id}")]
        // PUT api/City/{id}
        public async Task<IActionResult> UpdateCity(int id, UpdateCityDto cityDto)
        {
            await _uow.CityRepository.UpdateAsync(cityDto, id);
            await _uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpGet("{id}")]
        // GET api/City/{id}
        public async Task<IActionResult> GetCity(int id) { 
            var city = await _uow.CityRepository.GetAsync(id);
            return Ok(_mapper.Map<CityDto>(city)); }
    }
}
