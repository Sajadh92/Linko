using AutoMapper;

namespace Linko.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<MainEntity, DtoEntity>();

            CreateMap<UserProfile, UserManager>().ReverseMap();
            CreateMap<RegisterDto, UserProfile>().ReverseMap();
        }
    }
}
