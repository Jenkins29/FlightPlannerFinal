using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightPlannerFinal.Models;
using FlightPlannerFinal.Storage;

namespace FlightPlannerFinal.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetById(id);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            var validation = FlightStorage.FlightValidation(flight);
            if (!validation) return BadRequest();

            var isSame = FlightStorage.SameFlightValidation(flight);
            if (isSame) return Conflict();

            FlightStorage.AddFlight(flight);
            return Created("", flight);
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult Delete(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}