using AutoMapper;
using Master.Domain.Trips;

namespace Master.Trip.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTripRequestDto, CreateTripCommand>();

            CreateMap<TripDomain, TripDto>();

            CreateMap<UserTripDomain, UserTripDto>()
                .ForMember(x => x.From, o => o.MapFrom(x => x.From))
                .ForMember(x => x.To, o => o.MapFrom(x => x.To))
                .ForMember(x => x.Date, o => o.MapFrom(x => x.Date))
                .ForMember(x => x.User, o => o.MapFrom(x => x.User));

            CreateMap<BookTripRequestDto, BookTripCommand>();
            CreateMap<TripsListRequestDto, TripsListCommand>();
        }
    }
}
