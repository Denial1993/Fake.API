using Fake.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Fake.API.Dtos
{
    public class TouristRouteForCreationDto : IValidatableObject
    {
        [Required(ErrorMessage = "錯誤訊息:Title不可為空")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        //原價 X 折扣
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string? Features { get; set; }
        public string? Fees { get; set; }
        public string? Notes { get; set; }
        public double? Rating { get; set; }
        public string? TravelDays { get; set; }
        public string? TripType { get; set; }
        public string? DepartureCity { get; set; }
        public ICollection<TouristRoutePictureForCreationDto> TouristRoutePictures { get; set; } = new List<TouristRoutePictureForCreationDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult(
                    "路線名稱必須與敘述不同",
                    new[] { "TouristRouteForCreationDto" }
                                        );
            }
        }
    }
}
