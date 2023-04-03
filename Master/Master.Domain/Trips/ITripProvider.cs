using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Trips;

namespace Master.Domain.Trips
{
    public interface ITripProvider
    {
        Task<TripDomain> GetAsync(int id);

        Task<List<TripDomain>> GetAllAsync(TripsListCommand command);

        Task<TripDomain> CreateAsync(CreateTripCommand command);
    }
}
