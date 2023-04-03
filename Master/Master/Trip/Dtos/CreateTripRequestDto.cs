namespace Master.Trip.Dtos
{
    public class CreateTripRequestDto
    {
        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }
    }
}
