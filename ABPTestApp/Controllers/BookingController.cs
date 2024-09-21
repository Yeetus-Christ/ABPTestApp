using ABPTestApp.Dtos;
using ABPTestApp.Models;
using ABPTestApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ABPTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public ActionResult<decimal> AddBooking([FromBody] BookingDto booking)
        {
            decimal result = 0;

            try
            {
                result = _bookingService.AddBooking(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetBookings()
        {
            var result = _bookingService.GetBookings();

            return Ok(result);
        }
    }
}
