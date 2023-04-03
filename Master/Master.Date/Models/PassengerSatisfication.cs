using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Data.Models.Account;
using Master.Shared.Enums;

namespace Master.Data.Models
{
    internal class PassengerSatisfication
    {
        public int Id { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public TypeOfTravel TypeOfTravel { get; set; }

        public TravelClass Class { get; set; }

        public int FlightDistance { get; set; }

        public int WifiService { get; set; }

        public int ArrivalTime { get; set; }

        public int EaseOfBooking { get; set; }

        public int GateLocation { get; set; }

        public int FoodAndDrink { get; set; }

        public int OnlineBoarding { get; set; }

        public int SeatComfort { get; set; }

        public int InflightEntertainment { get; set; }

        public int OnBoardService { get; set; }

        public int LegRoomService { get; set; }

        public int BaggageHandling { get; set; }

        public int CheckinService { get; set; }

        public int InflightService { get; set; }

        public int Cleanliness { get; set; }

        public int DepartureDelayInMinutes { get; set; }

        public int ArrivalDelayInMinutes { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserAccount User { get; set; }
    }
}
