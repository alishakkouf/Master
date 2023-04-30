using AutoMapper;
using Master.Account.Dtos;
using Master.Domain.Accounts;
using Master.Domain.Passenger;

namespace Master.Passenger.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PassengerOpinionDomain, PassengerOpinionDto>();
            CreateMap<GetPassengerOpinionDto, GetPassengerOpinionDomain>();
        }
    }
}
