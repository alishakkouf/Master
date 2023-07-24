namespace Master.Trip.Dtos
{
    public class TripDto
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public int NumOfSeats { get; set; }

        public int AvailableNumOfSeats { get; set; }
    }
}
