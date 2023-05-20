using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;

namespace Master.Domain.Trips
{
    public interface ITripManager
    {
        Task<List<TripDomain>> GetAllAsync(TripsListCommand command);

        Task<TripDomain> CreateAsync(CreateTripCommand command);

        Task<TripDomain> UpdateAsync(UpdateTripCommand command);

        Task BookTripAsync(BookTripCommand command);

        Task<List<UserTripDomain>> GetMyTripsAsync();
    }
}
