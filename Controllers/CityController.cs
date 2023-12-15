using AutoMapper;
using ExampleProject.DAL;
using ExampleProject.DAL.Common;
using ExampleProject.Dtos;
using ExampleProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        // GET api/City
        public async Task<IActionResult> GetCities() { 
            var cities = await uow.CityRepository.GetAllAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }
        [HttpPost]
        // POST api/City
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            await uow.CityRepository.AddAsync(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        // DELETE api/City/{id}
        public async Task<IActionResult> DeleteCity(int id)
        {
            await uow.CityRepository.DeleteAsync(id);
            await uow.SaveAsync();
            return Ok(id);
        }

        [HttpPut("{id}")]
        // PUT api/City/{id}
        public async Task<IActionResult> UpdateCity(int id, UpdateCityDto cityDto)
        {
            await uow.CityRepository.UpdateAsync(cityDto, id);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpGet("{id}")]
        // GET api/City/{id}
        public async Task<IActionResult> GetCity(int id) { 
            var city = await uow.CityRepository.GetAsync(id);
            return Ok(mapper.Map<CityDto>(city)); }
    }
}
