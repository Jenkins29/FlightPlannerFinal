using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web2.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator<SearchFlightsRequest>> _searchFlightsRequestValidator;

        public CustomerController(IAirportService airportService, IFlightService flightService, IMapper mapper, 
            IEnumerable<IValidator<SearchFlightsRequest>> searchFlightsRequestValidator)
        {
            _airportService = airportService;
            _flightService = flightService;
            _mapper = mapper;
            _searchFlightsRequestValidator = searchFlightsRequestValidator;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult Search(string search)
        {
            AirportResponse[] airportRequests = new AirportResponse[1];
            var result = _airportService.SearchByPhrase(search);
            var airport = _mapper.Map<AirportResponse>(result);
            airportRequests[0] = airport;

            if (airport is not null) return Ok(airportRequests);

            return Ok();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            if (!_searchFlightsRequestValidator.All(v => v.IsValid(req))) return BadRequest();

            return Ok(_flightService.SearchFlightRequest(req));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchSingleFlight(int id)
        {
            var flight = _flightService.GetFullFLightById(id);
            var result = _mapper.Map<FlightResponse>(flight);

            if (result is null) return NotFound();

            return Ok(result);
        }

    }
}
