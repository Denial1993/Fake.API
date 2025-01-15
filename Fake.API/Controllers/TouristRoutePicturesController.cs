using AutoMapper;
using Fake.API.Dtos;
using Fake.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fake.API.Controllers
{
    [ApiController]
    [Route("api/touristroutes/{touristRouteId}/pictures")]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;

        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPictureListForTouristRoute(Guid touristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExist(touristRouteId))
            {
                return NotFound($"旅遊路徑{touristRouteId}不存在");
            }
            var picturesFromRepo = _touristRouteRepository.GetPicturesByTouristId(touristRouteId);
            if (picturesFromRepo == null || !picturesFromRepo.Any())
            {
                return NotFound("照片不存在");
            }
            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
        }
    }
}
