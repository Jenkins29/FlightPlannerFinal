using FlightPlanner.Core2.Models;

namespace FlightPlanner.Core2.Services
{
    public interface IDbServiceExtended : IDbService
    {
        void DeleteAll<T>() where T : Entity;

    }
}
