using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Passenger
{
    public interface IPassengerManager
    {
        Task<PassengerOpinionDomain> GetPassengerOpinion(GetPassengerOpinionDomain input);
    }
}
