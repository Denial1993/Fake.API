using Fake.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fake.API.Dtos
{
    public class TouristRoutePictureDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Guid TouristRouteId { get; set; }
    }
}
