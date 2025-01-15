//using Fake.API.Models;

//namespace Fake.API.Services
//{
//    public class MockTouristRouteRepository : ITouristRouteRepository
//    {
//        //私有旅遊路線列表，用來保存假數據的
//        private List<TouristRoute> _routes;
//        public MockTouristRouteRepository()
//        {
//            if (_routes == null)
//            {
//                InitializeTouristRoutes();
//            }
//        }
//        private void InitializeTouristRoutes()
//        {
//            _routes = new List<TouristRoute>
//            {
//                new TouristRoute {
//                    Id = Guid.NewGuid(),
//                    Title = "黄山",
//                    Description="黄山真好玩",
//                    OriginalPrice = 1299,
//                    Features = "<p>吃住行游购娱</p>",
//                    Fees = "<p>交通费用自理</p>",
//                    Notes="<p>小心危险</p>"
//                },
//                new TouristRoute {
//                    Id = Guid.NewGuid(),
//                    Title = "华山",
//                    Description="华山真好玩",
//                    OriginalPrice = 1299,
//                    Features = "<p>吃住行游购娱</p>",
//                    Fees = "<p>交通费用自理</p>",
//                    Notes="<p>小心危险</p>"
//                }
//            };
//        }
//        public IEnumerable<TouristRoute> GetTouristRoutes()
//        {
//            return _routes;
//        }

//        public TouristRoute GetTouristRoute(Guid touristRouteId)
//        {
//            return _routes.FirstOrDefault(x => x.Id == touristRouteId);
//        }

//    }
//}
