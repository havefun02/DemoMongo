using AutoMapper;

namespace App.Mappper
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile() {
            CreateMap<UserDto, User>();
        }
    }
}
