﻿using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class DbServiceExtended : DbService, IDbServiceExtended
    {
        public DbServiceExtended(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void DeleteAll<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }
    }
}
