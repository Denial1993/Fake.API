using AutoMapper;
using Fake.API.Dtos;
using Fake.API.Models;
using Fake.API.ResourceParameters;
using Fake.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
        [HttpHead]
        public IActionResult GetTouristRoutes([FromQuery] TouristRouteResourceParameters parameters)
        {
            var touristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes(
                parameters.Keyword, parameters.RatinOperator, parameters.RatingValue);

            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("沒有旅遊路線");
            }
            var touristRouteDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);
            return Ok(touristRouteDto);
        }

        [HttpGet("{touristRouteId}", Name = "GetTouristRouteById")]
        [HttpHead]
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
        [HttpPost]
        public IActionResult CreateTouristRoute([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
        {
            var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteForCreationDto);
            _touristRouteRepository.AddTouristRoute(touristRouteModel);
            _touristRouteRepository.Save();
            var touristRouteToReturn = _mapper.Map<TouristRouteDto>(touristRouteModel);
            return CreatedAtRoute("GetTouristRouteById", new { touristRouteId = touristRouteToReturn.Id }, touristRouteToReturn);
        }
        [HttpPut("{touristRouteId}")]
        public IActionResult UpdateTouristRoute(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto
            )
        {
            if (!_touristRouteRepository.TouristRouteExist(touristRouteId))
            {
                return NotFound("旅遊路線找不到");
            }
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            //接下來比較複雜就是:
            //1.映射 Dto
            //2.更新 Dto
            //3.映射 model
            _mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);
            _touristRouteRepository.Save();
            return NoContent();
        }
    }
}
