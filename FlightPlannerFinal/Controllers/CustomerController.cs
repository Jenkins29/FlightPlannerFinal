using System.Collections.Generic;
using System.Linq;
using FlightPlannerFinal.DbContext;
using FlightPlannerFinal.Models;
using FlightPlannerFinal.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerFinal.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public CustomerController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult Search(string search)
        {
            AirportRequest[] airportRequests = new AirportRequest[1];
            var result = FlightStorage.SearchAirport(search, _context);
            airportRequests[0] = result;
            if (result is not null) return Ok(airportRequests);

            return Ok();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            if (req.From == req.To)
            {
                return BadRequest();
            }

            List<Flight> flights = _context.Flights
                .Include(f => f.from)
                .Include(f => f.to)
                .ToList();

            return Ok(FlightStorage.SearchFlightRequest(req, flights));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchSingleFlight(int id)
        {
            var flight = _context.Flights
                .Include(a => a.to)
                .Include(a => a.from)
                .SingleOrDefault(f => f.Id == id);

            if (flight is null) return NotFound();

            return Ok(flight);
        }
    }
}