using System.Linq;
using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Airport SearchByPhrase(string phrase)
        {
            phrase = phrase.ToLower().Trim();

            var airport = Query().Where(a =>
                a.AirportCode.ToLower().Contains(phrase) ||
                a.City.ToLower().Contains(phrase) ||
                a.Country.ToLower().Contains(phrase));

            var result = airport.Select(a => new Airport{AirportCode = a.AirportCode, City = a.City, Country = a.Country})
                .ToList()[0];

            return result;
        }
    }
}
