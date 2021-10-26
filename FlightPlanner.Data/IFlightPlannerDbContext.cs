using FlightPlanner.Core2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightPlanner.Data
{
    public interface IFlightPlannerDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Flight> Flights { get; set; }
        DbSet<Airport> Airports { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
