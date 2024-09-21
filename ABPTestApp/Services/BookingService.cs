using ABPTestApp.Data;
using ABPTestApp.Dtos;
using ABPTestApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPTestApp.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public decimal AddBooking(BookingDto booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            var availableHall = _context.ConferenceHalls
                .Where(hall => hall.Id >= booking.HallId)
                .Where(hall => !hall.Bookings.Any(b =>
                    b.BookedAt.Date == booking.BookedAt.Date &&
                    ((booking.From >= b.From && booking.From < b.To) ||
                    (booking.To > b.From && booking.To <= b.To) ||
                    (booking.From <= b.From && booking.To >= b.To))))
                .ToList().FirstOrDefault();

            if (availableHall != null)
            {
                decimal totalPrice = 0;

                DateTime currentHour = booking.From;

                while (currentHour < booking.To)
                {
                    DateTime nextHour = currentHour.AddHours(1);
                    DateTime segmentEnd = nextHour < booking.To ? nextHour : booking.To;
                    double segmentDurationInHours = (segmentEnd - currentHour).TotalHours;

                    decimal hourlyRate = availableHall.RatePerHour * (decimal)segmentDurationInHours;

                    if (currentHour.Hour >= 6 && currentHour.Hour < 9)
                    {
                        hourlyRate *= 0.90m;
                    }
                    else if ((currentHour.Hour >= 9 && currentHour.Hour < 12) || (currentHour.Hour >= 14 && currentHour.Hour < 18))
                    {
                        hourlyRate *= 1.00m;
                    }
                    else if (currentHour.Hour >= 18 && currentHour.Hour < 23)
                    {
                        hourlyRate *= 0.80m;
                    }
                    else if (currentHour.Hour >= 12 && currentHour.Hour < 14)
                    {
                        hourlyRate *= 1.15m;
                    }

                    totalPrice += hourlyRate;
                    
                    currentHour = segmentEnd;
                }

                var services = _context.Services.Where(s => booking.ServiceIds.Contains(s.Id)).ToList();

                var newBooking = new Booking()
                {
                    HallId = booking.HallId,
                    BookedAt = booking.BookedAt,
                    From = booking.From,
                    To = booking.To,
                    TotalPrice = totalPrice + services.Sum(s => s.Price),
                    Services = services,
                };

                _context.Bookings.Add(newBooking);
                _context.SaveChanges();

                return newBooking.TotalPrice;
            }
            else
            {
                throw new Exception($"Hall with id {booking.HallId} is not available");    
            }    
        }

        public IEnumerable<Booking> GetBookings()
        {
            return _context.Bookings
                .Include(b => b.Services);
        }
    }
}
