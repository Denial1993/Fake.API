using Fake.API.Models;
using System.Collections;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Fake.API.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes(string keyword, string ratingOperator, int? ratingValue);
        TouristRoute GetTouristRoute(Guid touristRouteId);
        bool TouristRouteExist(Guid touristRouteId);
        IEnumerable<TouristRoutePicture> GetPicturesByTouristId(Guid touristRouteId);
        TouristRoutePicture GetPicture(int pictureId);
        void AddTouristRoute(TouristRoute touristRoute);
        void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture);
        void DeleteTouristRoute(TouristRoute touristRoute);
        bool Save();
    }
}
