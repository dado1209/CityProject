using AutoMapper;
using CityProject.Dtos;
using CityProject.Models;
using CityProject.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Data.Entity.Core;



namespace ExampleProject.Controllers
{
    [Route("api/cityParks")]
    [ApiController]
    public class CityParkController : ControllerBase
    {
        private readonly ICityParkService _cityParkService;
        private readonly IMapper _mapper;

        public CityParkController(ICityParkService cityParkService, IMapper mapper)
        {
            _cityParkService = cityParkService;
            _mapper = mapper;
        }

        [HttpPost]
        // POST api/CityParks
        public async Task<IActionResult> AddCityPark(CityParkDto cityParkDto)
        {
            try
            {
                var cityPark = _mapper.Map<CityPark>(cityParkDto);
                await _cityParkService.AddCityPark(cityPark);
                return StatusCode(201);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        // GET api/CityParks/{id}
        public async Task<IActionResult> GetCityPark(int id)
        {
            try
            {
                var cityPark = await _cityParkService.GetCityParkById(id);
                return Ok(_mapper.Map<CityParkDto>(cityPark));
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
        //GET api/CityParks
        public async Task<IActionResult> GetCityParks([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var cityParks = await _cityParkService.GetAllCityParks(sieveModel);
                return Ok(_mapper.Map<List<CityParkDto>>(cityParks));
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
                var updateCityPark = _mapper.Map<UpdateCityPark>(cityParkDto);
                await _cityParkService.UpdateCityPark(updateCityPark, id);
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


