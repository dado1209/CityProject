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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityParkController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        // POST api/CityPark
        public async Task<IActionResult> AddCityPark(CityParkDto cityParkDto)
        {
            var park = _mapper.Map<CityPark>(cityParkDto);
            await _uow.CityParkRepository.AddAsync(park);
            await _uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        // GET api/CityPark/{id}
        public async Task<IActionResult> GetCityPark(int id)
        {
            var park = await _uow.CityParkRepository.GetAsync(id);
            return Ok(_mapper.Map<CityParkDto>(park));
        }

        [HttpGet]
        // GET api/CityPark
        public async Task<IActionResult> GetCityParks()
        {
            var cityParks = await _uow.CityParkRepository.GetAllAsync();
            var cityParksDto = _mapper.Map<IEnumerable<CityParkDto>>(cityParks);
            return Ok(cityParksDto);
        }
    }
}


