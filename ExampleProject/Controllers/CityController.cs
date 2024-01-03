using AutoMapper;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Data.Entity.Core;

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
        // GET api/Cities
        public async Task<IActionResult> GetCities([FromQuery] SieveModel sieveModel) {
            return Ok(await _cityService.GetAllCities(sieveModel));
        }
        [HttpPost]
        // POST api/Cities
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            try {
                await _cityService.AddCity(cityDto);
                return StatusCode(201);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{id}")]
        // DELETE api/Cities/{id}
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                await _cityService.DeleteCity(id);
                return Ok(id);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        // PUT api/Cities/{id}
        public async Task<IActionResult> UpdateCity(int id, UpdateCityDto cityDto)
        {
            try
            {
                await _cityService.UpdateCity(cityDto, id);
                return StatusCode(200);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("{id}")]
        // GET api/Cities/{id}
        public async Task<IActionResult> GetCity(int id) {
            try
            {
                var city = await _cityService.GetCityById(id);
                return Ok(city);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
