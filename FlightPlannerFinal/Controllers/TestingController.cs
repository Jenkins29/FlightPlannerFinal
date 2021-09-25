using FlightPlannerFinal.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerFinal.Controllers
{

    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _context.RemoveRange(_context.Flights);
            _context.RemoveRange(_context.Airports);
            _context.SaveChanges();
            return Ok();
        }
    }
}