namespace Master.Trip.Dtos
{
    public class UpdateTripRequestDto
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }
    }
}
