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
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        [HttpGet]
        // GET api/Cities
        public async Task<IActionResult> GetCities([FromQuery] SieveModel sieveModel) {
            var cities = await _cityService.GetAllCities(sieveModel);
            return Ok(_mapper.Map<List<CityDto>>(cities));
        }
        [HttpPost]
        // POST api/Cities
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            try {
                var city = _mapper.Map<City>(cityDto);
                await _cityService.AddCity(city);
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
                var updateCity = _mapper.Map<UpdateCity>(cityDto);
                await _cityService.UpdateCity(updateCity, id);
                return StatusCode(200);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
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
                return Ok(_mapper.Map<CityDto>(city));
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
