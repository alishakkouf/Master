namespace Master.Passenger.Dtos
{
    public class GetPassengerOpinionDto
    {
        public string Gender { get; set; }
        public string CustomerType { get; set; }
        public string TypeOfTravel { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        public int InflightWifiService { get; set; }
        public int FlightDistance { get; set; }
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
        public string DepartureDelayInMinutes { get; set; }
        public string ArrivalDelayInMinutes { get; set; }
    }
}
