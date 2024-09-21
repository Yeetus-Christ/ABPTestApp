using System.ComponentModel.DataAnnotations;

namespace ABPTestApp.Dtos
{
    public class BookingDto
    {
        public DateTime BookedAt { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int HallId { get; set; }

        public List<int> ServiceIds { get; set; } = [];
    }
}
