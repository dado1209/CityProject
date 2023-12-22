using AutoMapper;
using CityProject.DAL.Common;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        // GET api/City
        public async Task<IActionResult> GetCities() { 
            var cities = await _cityService.GetAllCities();
            return Ok(cities);
        }
        [HttpPost]
        // POST api/City
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            await _cityService.AddCity(cityDto);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        // DELETE api/City/{id}
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _cityService.DeleteCity(id);
            return Ok(id);
        }

        [HttpPut("{id}")]
        // PUT api/City/{id}
        public async Task<IActionResult> UpdateCity(int id, UpdateCityDto cityDto)
        {
            await _cityService.UpdateCity(cityDto, id);
            return StatusCode(200);
        }

        [HttpGet("{id}")]
        // GET api/City/{id}
        public async Task<IActionResult> GetCity(int id) { 
            var city = await _cityService.GetCityById(id);
            return Ok(city); }
    }
}
