using FlightPlannerFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerFinal.DbContext
{
    public class FlightPlannerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
        {
            
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<AirportRequest> Airports { get; set; }

    }
}
