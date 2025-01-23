using Fake.API.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Fake.API.ValidationAttributes
{
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,  //輸入的數據對象
            ValidationContext validationContext //驗證的上下文關係對象
        )
        {
            var touristRouteDto = (TouristRouteForManipulationDto)validationContext.ObjectInstance; //通過上下文關係,獲得當前的對象
            if (touristRouteDto.Title == touristRouteDto.Description)
            {
                return new ValidationResult(
                    "路線名稱必須與敘述不同",
                    new[] { "TouristRouteForManipulationDto" }
                );
            }
            return ValidationResult.Success;
        }
    }
}
