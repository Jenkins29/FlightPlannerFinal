using FlightPlanner.Core2.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base (options)
        {
            
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }
    }
}
