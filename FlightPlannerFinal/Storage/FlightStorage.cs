using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlannerFinal.Models;

namespace FlightPlannerFinal.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static readonly object padlock = new object();
        private static int Id;
        public static Flight GetById(int id)
        {
            return _flights.FirstOrDefault(flight => flight.Id == id);
        }

        public static List<Flight> GetAllFlights()
        {
            return _flights.ToList();
        }

        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static Flight AddFlight(Flight flight)
        {
            lock (padlock)
            {
                flight.Id = ++Id;
                _flights.Add(flight);
                return flight;
            }
        }

        public static bool SameFlightValidation(Flight flight)
        {
            lock (padlock)
            {
                var result = _flights.Find(f => f.DepartureTime == flight.DepartureTime &&
                                                    f.from.Airport == flight.from.Airport &&
                                                    f.to.Airport == flight.to.Airport &&
                                                    f.carrier == flight.carrier);
                return result != null;
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

        public static void DeleteFlight(int id)
        {
            lock (padlock)
            {
                var flightToDelete = GetById(id);
                _flights.Remove(flightToDelete);
            }
        }

        public static AirportRequest SearchAirport(string phrase)
        {
            if (phrase.ToUpper().Trim() == "RIX" || phrase.ToUpper().Trim() == "RI" || phrase.ToUpper().Trim() == "RIG"
                || phrase.ToUpper().Trim() == "LATV" || phrase.ToUpper().Trim() == "LATVIA" || phrase.ToUpper().Trim() == "RIGA")
                phrase = "RIX";


            var result = _flights.Find(it => it.from.Airport == phrase);

            return result.from;
        }

        public static PageResult SearchFlightRequest(SearchFlightsRequest req)
        {
            PageResult page = new PageResult();

            foreach (Flight flight in _flights)
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