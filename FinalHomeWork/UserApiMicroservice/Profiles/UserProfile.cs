using AutoMapper;
using Domain.Models;
using UserApiMicroservice.Dtos;

namespace UserApiMicroservice.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Credentials, cfg => cfg.MapFrom(ori => ori.Password));
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Credentials, cfg => cfg.MapFrom(ori => ori.Password));
            CreateMap<User, UserSimpleDto>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
