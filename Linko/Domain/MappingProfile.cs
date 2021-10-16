using AutoMapper;

namespace Linko.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<MainEntity, DtoEntity>();

            CreateMap<UserProfile, UserManager>().ReverseMap();
            CreateMap<UserProfile, RegisterDto>().ReverseMap();
            CreateMap<Account, InsertAccountDto>().ReverseMap();
            CreateMap<Account, UpdateAccountDto>().ReverseMap();
        }
    }
}
