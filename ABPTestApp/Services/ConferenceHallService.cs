using ABPTestApp.Data;
using ABPTestApp.Dtos;
using ABPTestApp.Models;
using System;

namespace ABPTestApp.Services
{
    public class ConferenceHallService
    {
        private readonly AppDbContext _context;

        public ConferenceHallService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IEnumerable<ConferenceHall> GetConferenceHalls(DateTime date, DateTime from, DateTime to, int capacity) 
        {
            var availableHalls = _context.ConferenceHalls
            .Where(hall => hall.Capacity >= capacity)
            .Where(hall => !hall.Bookings.Any(booking =>
                booking.BookedAt.Date == date.Date &&
                ((from >= booking.From && from < booking.To) ||
                 (to > booking.From && to <= booking.To) ||
                 (from <= booking.From && to >= booking.To))))
            .ToList();

            return availableHalls;
        }

        public int AddConferenceHall (ConferenceHallDto conferenceHall)
        {
            if (conferenceHall == null)
            {
                throw new ArgumentNullException(nameof(conferenceHall));
            }

            _context.ConferenceHalls.Add(new ConferenceHall() { Name = conferenceHall.Name, Capacity = conferenceHall.Capacity, RatePerHour = conferenceHall.RatePerHour });
            _context.SaveChanges();

            return _context.ConferenceHalls.OrderBy(x => x.Id).Last().Id;
        }

        public void DeleteConferenceHall (int id)
        {
            var conferenceHall = _context.ConferenceHalls.FirstOrDefault(x => x.Id == id);

            if (conferenceHall != null)
            {
                _context.ConferenceHalls.Remove(conferenceHall);
                _context.SaveChanges();
            }
            else 
            {
                throw new Exception($"Hall with id {id} doesn't exist");
            }
        }

        public void UpdateConferenceHall(ConferenceHallDto conferenceHall)
        {
            if (conferenceHall == null)
            {
                throw new ArgumentNullException(nameof(conferenceHall));
            }

            if (conferenceHall.RatePerHour <=0 || conferenceHall.Capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(conferenceHall), "Parameters can't be less than or equal to 0");
            }

            var conferenceHall1 = _context.ConferenceHalls.FirstOrDefault(x => x.Id == conferenceHall.Id);

            if (conferenceHall1 != null)
            {
                conferenceHall1.Name = conferenceHall.Name;
                conferenceHall1.RatePerHour = conferenceHall.RatePerHour;
                conferenceHall1.Capacity = conferenceHall.Capacity;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Hall with id {conferenceHall.Id} doesn't exist");
            }
        }
    }
}
