using FlightPlannerFinal.Models;
using FlightPlannerFinal.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerFinal.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult Search(string search)
        {
            AirportRequest[] airportRequests = new AirportRequest[1];
            var result = FlightStorage.SearchAirport(search);
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

            return Ok(FlightStorage.SearchFlightRequest(req));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchSingleFlight(int id)
        {
            var flight = FlightStorage.GetById(id);
            if (flight is null) return NotFound();

            return Ok(flight);
        }
    }
}