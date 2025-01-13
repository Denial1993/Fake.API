using Fake.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using static Azure.Core.HttpHeader;
using Newtonsoft.Json;

namespace Fake.API.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TouristRoute> TouristRoutes { get; set; }
        public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
            //{
            //    Id = Guid.Parse("b0be8db8-3513-4142-ae0e-0595543dba1f"),  // 固定 GUID
            //    Title = "Sample Tourist Route",  // 固定標題
            //    Description = "This is a description for the sample tourist route.",  // 固定描述
            //    OriginalPrice = 1000,  // 固定價格
            //    CreateTime = new DateTime(2025, 1, 9, 8, 46, 53),  // 固定時間
            //    UpdateTime = null,  // 可以是 null
            //    DepartureTime = new DateTime(2025, 5, 1),  // 固定出發時間
            //    Features = "Sample features",  // 固定特色
            //    Fees = "Sample fees",  // 固定費用
            //    Notes = "Sample notes"  // 固定備註
            //});
            var touristRouteJsonData =  File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ @"\Database\\touristRoutesMockData.json");
            IList<TouristRoute> touristRoutes = JsonConvert.DeserializeObject<IList<TouristRoute>>(touristRouteJsonData);
            modelBuilder.Entity<TouristRoute>().HasData(touristRoutes);
            
            var touristRoutePictureJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Database\\touristRoutePicturesMockData.json");
            IList<TouristRoutePicture> touristPictureRoutes = JsonConvert.DeserializeObject<IList<TouristRoutePicture>>(touristRoutePictureJsonData);
            modelBuilder.Entity<TouristRoutePicture>().HasData(touristPictureRoutes);

            base.OnModelCreating(modelBuilder);
        }
    }
}
