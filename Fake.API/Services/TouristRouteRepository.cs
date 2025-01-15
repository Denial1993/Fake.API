using Fake.API.Database;
using Fake.API.Models;

namespace Fake.API.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;
        public TouristRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TouristRoutePicture> GetPicturesByTouristId(Guid touristRouteId)
        {
            return _context.TouristRoutePictures.Where(p =>p.TouristRouteId == touristRouteId).ToList();
        }

        public TouristRoute GetTouristRoute(Guid touristRouteId)
        {
            return _context.TouristRoutes.FirstOrDefault(n => n.Id == touristRouteId);

        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
            return _context.TouristRoutes;
        }

        public bool TouristRouteExist(Guid touristRouteId)
        {
            return _context.TouristRoutes.Any(t => t.Id == touristRouteId);
        }
    }
}
