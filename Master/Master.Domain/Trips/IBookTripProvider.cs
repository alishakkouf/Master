using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Trips
{
    public interface IBookTripProvider
    {
        Task BookTripAsync(int userId, int id);

        Task<List<UserTripDomain>> GetMyTripsAsync(int userId);
    }
}
