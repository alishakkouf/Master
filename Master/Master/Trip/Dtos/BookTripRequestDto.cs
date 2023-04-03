using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Master.Trip.Dtos
{
    public class BookTripRequestDto
    {
        [Required]
        public int TripId { get; set; }
    }
}
