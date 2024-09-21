using System.ComponentModel.DataAnnotations;

namespace ABPTestApp.Models
{
    public class Booking
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime BookedAt { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Required]
        public int HallId { get; set; }

        [Required]
        public decimal TotalPrice { get; set; } = 0;

        public List<Service> Services { get; set; } = [];

        public ConferenceHall? ConferenceHall { get; set; } = null!;
    }
}
