using System.Linq;
using FlightPlannerFinal.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightPlannerFinal.Models;
using FlightPlannerFinal.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerFinal.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(a => a.to)
                .Include(a => a.from)
                .SingleOrDefault(f => f.Id == id);

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

            var isSame = FlightStorage.SameFlightValidation(flight, _context);
            if (isSame) return Conflict();

            _context.Flights.Add(flight);
            _context.SaveChanges();

            return Created("", flight);
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult Delete(int id)
        {
            var flight = _context.Flights
                .Include(a => a.to)
                .Include(a => a.from)
                .SingleOrDefault(f => f.Id == id);

            if (flight != null)
            {
                _context.Airports.Remove(flight.to);
                _context.Airports.Remove(flight.from);
                _context.Flights.Remove(flight);

                _context.SaveChanges();
            }
            return Ok();
        }
    }
}