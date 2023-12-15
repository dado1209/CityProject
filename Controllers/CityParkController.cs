using AutoMapper;
using ExampleProject.DAL;
using ExampleProject.DAL.Common;
using ExampleProject.Dtos;
using ExampleProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ExampleProject.Controllers
{
    [Route("api/cityParks")]
    [ApiController]
    public class CityParkController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityParkController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPost]
        // POST api/CityPark
        public async Task<IActionResult> AddCityPark(CityParkDto cityParkDto)
        {
            var park = mapper.Map<CityPark>(cityParkDto);
            await uow.CityParkRepository.AddAsync(park);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        // GET api/CityPark/{id}
        public async Task<IActionResult> GetCityPark(int id)
        {
            var park = await uow.CityParkRepository.GetAsync(id);
            return Ok(mapper.Map<CityParkDto>(park));
        }

        [HttpGet]
        // GET api/CityPark
        public async Task<IActionResult> GetCityParks()
        {
            var cityParks = await uow.CityParkRepository.GetAllAsync();
            var cityParksDto = mapper.Map<IEnumerable<CityParkDto>>(cityParks);
            return Ok(cityParksDto);
        }
    }
}


