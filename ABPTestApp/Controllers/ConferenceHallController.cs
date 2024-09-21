using ABPTestApp.Dtos;
using ABPTestApp.Models;
using ABPTestApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABPTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceHallController : ControllerBase
    {
        private readonly ConferenceHallService _conferenceHallService;

        public ConferenceHallController(ConferenceHallService conferenceHallService)
        {
            _conferenceHallService = conferenceHallService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ConferenceHall>> GetConferenceHalls(DateTime date, DateTime from, DateTime to, int capacity)
        {
            var result = _conferenceHallService.GetConferenceHalls(date, from, to, capacity);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<string> AddConferenceHall([FromBody] ConferenceHallDto conferenceHall) 
        {
            int result;

            try
            {
                result = _conferenceHallService.AddConferenceHall(conferenceHall);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete]
        public ActionResult DeleteConferenceHall(int id) 
        {
            try
            {
                _conferenceHallService.DeleteConferenceHall(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateConferenceHall(ConferenceHallDto conferenceHall) 
        {
            try
            {
                _conferenceHallService.UpdateConferenceHall(conferenceHall);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
