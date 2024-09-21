using System.ComponentModel.DataAnnotations;

namespace ABPTestApp.Models
{
    public class ConferenceHall
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public decimal RatePerHour { get; set; }

        public List<Booking> Bookings { get; set; } = [];
    }
}
