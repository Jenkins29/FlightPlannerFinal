using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFLightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            var result = Query()
                .Include(f => f.To)
                .Include(f => f.From).Any(f => f.DepartureTime.Trim().ToUpper() == flight.DepartureTime.Trim().ToUpper()
                                                && f.ArrivalTime.Trim().ToUpper() == flight.ArrivalTime.Trim().ToUpper()
                                                && f.Carrier.Trim().ToUpper() == flight.Carrier.Trim().ToUpper()
                                                && f.From.AirportCode.Trim().ToUpper() == flight.From.AirportCode.Trim().ToUpper()
                                                && f.To.AirportCode.Trim().ToUpper() == flight.To.AirportCode);

            var lid = flight;
            return result;
        }

        public PageResult SearchFlightRequest(SearchFlightsRequest req)
        {
            PageResult page = new PageResult();

            page.Items = Query()
                .Include(f => f.From)
                .Include(f => f.To)
                .Where(f => f.DepartureTime.Contains(req.DepartureDate) &&
                            f.From.AirportCode == req.From
                                            && f.To.AirportCode == req.To).ToList();
            page.TotalItems = page.Items.Count;

            return page;
        }
    }
}
