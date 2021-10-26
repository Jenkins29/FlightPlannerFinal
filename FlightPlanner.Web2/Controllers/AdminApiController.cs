using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core2.DTO;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlightPlanner.Web2.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator<FlightRequest>> _validator;
        private static readonly object padlock = new object();

        public AdminApiController(IFlightService flightService, IMapper mapper, IEnumerable<IValidator<FlightRequest>> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFLightById(id);
            if (flight == null)
                return NotFound();

            return Ok(_mapper.Map<FlightResponse>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(FlightRequest request)
        {
            lock (padlock)
            {
                if (!_validator.All(v => v.IsValid(request))) return BadRequest();

                var flight = _mapper.Map<Flight>(request);

                if (_flightService.Exists(flight)) return Conflict();

                _flightService.Create(flight);

                return Created(uri: "", _mapper.Map<FlightResponse>(flight)); 
            }

        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (padlock)
            {
                var flight = _flightService.GetFullFLightById(id);

                if (flight != null) _flightService.Delete(flight);

                return Ok();  
            }
            
        }
    }
}
