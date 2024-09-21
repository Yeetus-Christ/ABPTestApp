using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ABPTestApp.Models
{
    public class Service
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [JsonIgnore]
        public List<Booking> Bookings { get; set; } = [];
    }
}
