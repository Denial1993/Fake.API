using AutoMapper;
using Fake.API.Dtos;
using Fake.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fake.API.Controllers
{
    [Route("api/[controller]")] //api/touristroute
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ITouristRouteRepository _touristRouteRepository;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _touristRouteRepository = touristRouteRepository;
        }
        [HttpGet]
        public IActionResult GetTouristRoutes()
        {
            var touristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes();
            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0) { return NotFound("沒有旅遊路線"); }
            var touristRouteDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);
            return Ok(touristRouteDto);
        }
        [HttpGet("{touristRouteId}")]
        public IActionResult GetTouristRouteById(Guid touristRouteId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            if (touristRouteFromRepo == null)
            {
                return NotFound($"旅遊路線{touristRouteId}找不到");
            }

            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
            return Ok(touristRouteDto);
        }
    }
}
