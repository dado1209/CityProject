using AutoMapper;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core;



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
        // POST api/CityParks
        public async Task<IActionResult> AddCityPark(CityParkDto cityParkDto)
        {
            try
            {
                await _cityParkService.AddCityPark(cityParkDto);
                return StatusCode(201);
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
        // GET api/CityParks/{id}
        public async Task<IActionResult> GetCityPark(int id)
        {
            try
            {
                return Ok(await _cityParkService.GetCityParkById(id));
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

        [HttpGet]
        // GET api/CityParks
        public async Task<IActionResult> GetCityParks()
        {
            try
            {
                return Ok(await _cityParkService.GetAllCityParks());
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

        [HttpDelete("{id}")]
        // DELETE api/CityParks/{id}
        public async Task<IActionResult> DeleteCityPark(int id)
        {
            try
            {
                await _cityParkService.DeleteCityPark(id);
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
        // PUT api/CityParks/{id}
        public async Task<IActionResult> UpdateCityPark(int id, UpdateCityParkDto cityParkDto)
        {
            try
            {
                await _cityParkService.UpdateCityPark(cityParkDto, id);
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
    }
}


