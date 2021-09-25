using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlannerFinal.DbContext;
using FlightPlannerFinal.Models;

namespace FlightPlannerFinal.Storage
{
    public static class FlightStorage
    {
        private static readonly object padlock = new object();
        public static bool SameFlightValidation(Flight flight, FlightPlannerDbContext context)
        {
            lock (padlock)
            {
                var result = context.Flights.Any(f => f.from.Airport == flight.from.Airport &&
                                                      f.to.Airport == flight.to.Airport &&
                                                      f.DepartureTime == flight.DepartureTime &&
                                                      f.carrier == flight.carrier);

                return result;
            }
        }

        public static bool FlightValidation(Flight flight)
        {

            if (flight.from is null ||
                flight.to is null ||
                String.IsNullOrEmpty(flight.carrier) ||
                flight.DepartureTime is null ||
                flight.ArrivalTime is null) return false;

            if (String.IsNullOrEmpty(flight.from.Airport) ||
                String.IsNullOrEmpty(flight.to.Airport) ||
                String.IsNullOrEmpty(flight.from.City) ||
                String.IsNullOrEmpty(flight.to.City) ||
                String.IsNullOrEmpty(flight.from.Country) ||
                String.IsNullOrEmpty(flight.to.Country)) return false;

            if (flight.from.Airport.Trim().ToLower() == flight.to.Airport.Trim().ToLower()) return false;

            if (Convert.ToDateTime(flight.DepartureTime) >= Convert.ToDateTime(flight.ArrivalTime)) return false;

            return true;
        }

        public static AirportRequest SearchAirport(string phrase, FlightPlannerDbContext context)
        {
            if (phrase.ToUpper().Trim() == "RIX" || phrase.ToUpper().Trim() == "RI" || phrase.ToUpper().Trim() == "RIG"
                || phrase.ToUpper().Trim() == "LATV" || phrase.ToUpper().Trim() == "LATVIA" || phrase.ToUpper().Trim() == "RIGA")
                phrase = "RIX";

            var result = context.Airports.Where(a => a.Airport == phrase).Single();

            return result;
        }

        public static PageResult SearchFlightRequest(SearchFlightsRequest req, List<Flight> flights)
        {
            PageResult page = new PageResult();
            
                foreach (Flight flight in flights)
                {
                    if (req.From == flight.from.Airport &&
                        req.To == flight.to.Airport)
                    {
                        page.Items.Add(flight);
                        page.Page = page.Items.Count;
                        page.TotalItems = page.Items.Count;
                        return page;
                    }
                }

            return page;
        }
    }
}