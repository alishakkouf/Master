using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Passenger
{
    public class GetPassengerOpinionDomain
    {
        public string Gender { get; set; }
        public string CustomerType { get; set; }
        public string TypeOfTravel { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        public string AgeClass => Age <= 18 ? "child" : "adult";
        public int InflightWifiService { get; set; }
        public int FlightDistance { get; set; }
        public string FlightDistanceClass => FlightDistance <= 1000 ? "less than or equal 1000" : FlightDistance <= 2000 ? "less than or equal 2000" : FlightDistance <= 3000 ? "less than or equal 3000" : FlightDistance <= 4000 ? "less than or equal 4000" : "less than or equal 5000";
        public int DepartureArrivalTimeConvenient { get; set; }
        public int EaseOfOnlineBooking { get; set; }
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
        public string DepartureDelayInMinutesClass => DepartureDelayInMinutes <= 15 ? "less than or equal 15" : DepartureDelayInMinutes <= 30 ? "less than or equal 30" : DepartureDelayInMinutes <= 45 ? "less than or equal 45" : DepartureDelayInMinutes <= 60 ? "less than or equal 60" : "more than 60";
        public int ArrivalDelayInMinutes { get; set; }

        public string ArrivalDelayInMinutesClass => ArrivalDelayInMinutes <= 15 ? "less than or equal 15" : ArrivalDelayInMinutes <= 30 ? "less than or equal 30" : ArrivalDelayInMinutes <= 45 ? "less than or equal 45" : ArrivalDelayInMinutes <= 60 ? "less than or equal 60" : "more than 60";
        //public string Satisfaction { get; set; }

    }
}
