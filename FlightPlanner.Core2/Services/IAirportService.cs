using FlightPlanner.Core2.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Core2.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        Airport SearchByPhrase(string phrase);
    }
}
