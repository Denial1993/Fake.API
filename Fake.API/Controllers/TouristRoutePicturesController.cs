using AutoMapper;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
