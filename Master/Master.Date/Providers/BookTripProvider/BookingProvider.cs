using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Master.Data.Models.Trips;
using Master.Domain.Trips;
using Master.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Master.Data.Providers.BookTripProvider
{
    internal class BookingProvider : GenericProvider<BookedTrip>, IBookTripProvider
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public BookingProvider(MasterDbContext dbContext,
            IMapper mapper,
            IStringLocalizerFactory factory)
        {
            DbContext = dbContext;
            _mapper = mapper;
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task BookTripAsync(int userId, int id)
        {
           var toBeCreated = new BookedTrip 
           {
               TripId = id,
               UserId= userId
           };

            await DbContext.BookedTrips.AddAsync(toBeCreated);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<UserTripDomain>> GetMyTripsAsync(int userId)
        {
            var data =await ActiveDbSet.Where(x=>x.UserId == userId)
                                       .Include(x=>x.Trip)
                                       .Include(x=>x.User).ThenInclude(x=>x.UserRoles).ToListAsync();

            return _mapper.Map<List<UserTripDomain>>(data);
        }
    }
}
