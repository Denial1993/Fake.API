using Fake.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Fake.API.Dtos
{
    public class TouristRouteForUpdateDto : TouristRouteForManipulationDto
    {
        [Required(ErrorMessage ="更新必備")]
        [MaxLength(1500)]
        public override string Description { get; set; }
    }
}
