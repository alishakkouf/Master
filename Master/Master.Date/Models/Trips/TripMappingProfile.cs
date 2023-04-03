using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Master.Domain.Trips;

namespace Master.Data.Models.Trips
{
    public class TripMappingProfile : Profile
    {
        public TripMappingProfile()
        {
            CreateMap<Trip, TripDomain>().ReverseMap();

            CreateMap<BookedTrip, UserTripDomain>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Trip.Id))
                .ForMember(x => x.From, o => o.MapFrom(x => x.Trip.From))
                .ForMember(x => x.To, o => o.MapFrom(x => x.Trip.To))
                .ForMember(x => x.Date, o => o.MapFrom(x => x.Trip.Date))
                .ForMember(x => x.User, o => o.MapFrom(x => x.User));
        }
    }
}
