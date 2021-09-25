using API.DTOs;
using API.Entites;
using API.Extensions;
using AutoMapper;
using System.Linq;
namespace API.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
                        src => src.MapFrom(u => u.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, src => src.MapFrom(x => x.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
        }
    }
}