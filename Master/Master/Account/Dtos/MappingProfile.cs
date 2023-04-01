using AutoMapper;
using Master.Domain.Accounts;

namespace Master.Account.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterInputDto, RegisterInputCommand>();
        }
    }
}
