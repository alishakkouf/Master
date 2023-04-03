using Master.Account.Dtos;
using Master.Domain.Accounts;

namespace Master.Trip.Dtos
{
    public class UserTripDto
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public UserAccountDto User { get; set; }
    }
}
