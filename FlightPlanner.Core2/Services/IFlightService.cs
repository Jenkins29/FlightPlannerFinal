using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Models;

namespace FlightPlanner.Core2.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFLightById(int id);
        bool Exists(Flight flight);
        PageResult SearchFlightRequest(SearchFlightsRequest req);
    }
}
