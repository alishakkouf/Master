using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;
using Master.Domain.Trips;
using Master.Shared;
using Master.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using static System.Formats.Asn1.AsnWriter;

namespace Master.Manager.TripsManager
{
    public class TripManager : ITripManager
    {
        private readonly IBookTripProvider _bookTripProvider;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITripProvider _tripProvider;
        private readonly IStringLocalizer _localizer;

        public TripManager(ITripProvider tripProvider,
            ICurrentUserService currentUserService,
            IBookTripProvider bookTripProvider,
            IStringLocalizerFactory factory)
        {
            _currentUserService = currentUserService;
            _tripProvider = tripProvider;
            _bookTripProvider = bookTripProvider;
            _localizer = factory.Create(typeof(CommonResource));

        }

        public async Task BookTripAsync(BookTripCommand command)
        {
           var trip = await _tripProvider.GetAsync(command.TripId);

            if (trip.AvailableNumOfSeats == 0)
            {
                throw new BusinessException(_localizer["There is no available seats!!"]);
            }

           var userId = _currentUserService.GetUserId();

           await _bookTripProvider.BookTripAsync(userId.Value, command.TripId);
        }

        public async Task<List<UserTripDomain>> GetMyTripsAsync() 
        {
            var userId = _currentUserService.GetUserId();

            return await _bookTripProvider.GetMyTripsAsync(userId.Value);
        }
        public async Task<TripDomain> CreateAsync(CreateTripCommand command)
        {
            return await _tripProvider.CreateAsync(command);
        }

        public async Task<TripDomain> UpdateAsync(UpdateTripCommand command)
        {
            return await _tripProvider.UpdateAsync(command);
        }

        public async Task<List<TripDomain>> GetAllAsync(TripsListCommand command)
        {
           return await _tripProvider.GetAllAsync(command);
        }
    }
}
