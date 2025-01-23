using AutoMapper;
using Fake.API.Dtos;
using Fake.API.Models;
using Fake.API.Services;
namespace Fake.API.Profiles
{
    public class TouristRoutePictureProfile : Profile
    {

        public TouristRoutePictureProfile()
        {
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
            CreateMap<TouristRoutePictureForCreationDto,TouristRoutePicture>();
            CreateMap<TouristRoutePicture, TouristRoutePictureForCreationDto>();
        }
    }
}
