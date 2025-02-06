using AutoMapper;
using Fake.API.Dtos;
using Fake.API.Models;
using Fake.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fake.API.Controllers
{
    [Route("api/touristroutes/{touristRouteId}/pictures")]
    [ApiController]
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
        public async Task<IActionResult> GetPictureListForTouristRoute(Guid touristRouteId)
        {                
            if (!(await _touristRouteRepository.TouristRouteExistAsync(touristRouteId)))
            {
                return NotFound($"旅遊路徑{touristRouteId}不存在");
            }
            var picturesFromRepo = await _touristRouteRepository.GetPicturesByTouristIdAsync(touristRouteId);
            if (picturesFromRepo == null || !picturesFromRepo.Any())
            {
                return NotFound("照片不存在");
            }
            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
        }

        [HttpGet("{pictureId}", Name = "GetPicture")]
        public async Task<IActionResult> GetPicture(Guid touristRouteId, int pictureId)
        {
            if (!await (_touristRouteRepository.TouristRouteExistAsync(touristRouteId)))
            {
                return NotFound($"旅遊路徑{touristRouteId}不存在");
            }
            var pictureFromRepo = _touristRouteRepository.GetPictureAsync(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound("相片不存在");
            }
            return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
        }
        [HttpPost]
        public async Task<IActionResult> CreateTouristRoutePicture([FromRoute] Guid touristRouteId, [FromBody] TouristRoutePictureForCreationDto touristRoutePictureForCreationDto)
        {
            if (!await (_touristRouteRepository.TouristRouteExistAsync(touristRouteId)))
            {
                return NotFound($"旅遊路徑{touristRouteId}不存在");
            }
            var pictureModel = _mapper.Map<TouristRoutePicture>(touristRoutePictureForCreationDto);
            _touristRouteRepository.AddTouristRoutePicture(touristRouteId, pictureModel);
            _touristRouteRepository.SaveAsync();
            var pictureToReturn = _mapper.Map<TouristRoutePictureDto>(pictureModel);
            return CreatedAtRoute(
                "GetPicture",
                new
                {
                    touristRouteId = pictureModel.TouristRouteId,
                    pictureId = pictureModel.Id
                }, pictureToReturn
            );
        }
        [HttpDelete("{pictureId}")]
        public async Task<IActionResult> DeletePicture(
            [FromRoute] Guid touristRouteId,
            [FromRoute] int pictureId
            )
        {
            if (!await (_touristRouteRepository.TouristRouteExistAsync(touristRouteId)))
            {
                return NotFound($"旅遊路徑{touristRouteId}不存在");
            }
            var picture = await _touristRouteRepository.GetPictureAsync(pictureId);
            _touristRouteRepository.DeleteTouristRoutePicture(picture);
            _touristRouteRepository.SaveAsync();
            return NoContent(); //請求成功,但無響應值
        }
    }
}
