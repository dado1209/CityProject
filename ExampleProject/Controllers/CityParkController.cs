using AutoMapper;
using CityProject.DAL.Common;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ExampleProject.Controllers
{
    [Route("api/cityParks")]
    [ApiController]
    public class CityParkController : ControllerBase
    {
        private readonly ICityParkService _cityParkService;

        public CityParkController(ICityParkService cityParkService)
        {
            _cityParkService = cityParkService;
        }

        [HttpPost]
        // POST api/CityPark
        public async Task<IActionResult> AddCityPark(CityParkDto cityParkDto)
        {
            await _cityParkService.AddCityPark(cityParkDto);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        // GET api/CityPark/{id}
        public async Task<IActionResult> GetCityPark(int id)
        {
            return Ok(await _cityParkService.GetCityParkById(id));
        }

        [HttpGet]
        // GET api/CityPark
        public async Task<IActionResult> GetCityParks()
        {
            return Ok(await _cityParkService.GetAllCityParks());
        }

        [HttpDelete("{id}")]
        // DELETE api/CityPark/{id}
        public async Task<IActionResult> DeleteCityPark(int id)
        {
            await _cityParkService.DeleteCityPark(id);
            return Ok(id);
        }

        [HttpPut("{id}")]
        // PUT api/City/{id}
        public async Task<IActionResult> UpdateCityPark(int id, UpdateCityParkDto cityParkDto)
        {
            await _cityParkService.UpdateCityPark(cityParkDto, id);
            return StatusCode(200);
        }
    }
}


