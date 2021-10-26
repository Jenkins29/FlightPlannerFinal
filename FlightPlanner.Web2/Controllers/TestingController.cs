using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web2.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IDbServiceExtended _service;

        public TestingController(IDbServiceExtended service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _service.DeleteAll<Flight>();
            _service.DeleteAll<Airport>();

            return Ok();
        }
    }
}
