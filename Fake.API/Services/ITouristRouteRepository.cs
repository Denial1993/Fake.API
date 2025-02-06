using Fake.API.Models;
using System.Collections;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Threading.Tasks;
namespace Fake.API.Services
{
    public interface ITouristRouteRepository
    {
        Task<IEnumerable<TouristRoute> GetTouristRoutesAsync(string keyword, string ratingOperator, int? ratingValue);
        Task<TouristRoute> GetTouristRouteAsync(Guid touristRouteId);
        Task<bool> TouristRouteExistAsync(Guid touristRouteId);
        Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristIdAsync(Guid touristRouteId);
        Task<TouristRoutePicture> GetPictureAsync(int pictureId);
        void AddTouristRoute(TouristRoute touristRoute);
        void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture);
        void DeleteTouristRoute(TouristRoute touristRoute);
        void DeleteTouristRoutePicture(TouristRoutePicture picture);
        Task<bool> SaveAsync();

    }
}
