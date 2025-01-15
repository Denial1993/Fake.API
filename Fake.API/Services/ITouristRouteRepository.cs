using Fake.API.Models;
using System.Collections;

namespace Fake.API.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        TouristRoute GetTouristRoute(Guid touristRouteId);
        bool TouristRouteExist(Guid touristRouteId);
        IEnumerable<TouristRoutePicture> GetPicturesByTouristId(Guid touristRouteId);
    }
}
