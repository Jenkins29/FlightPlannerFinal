using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Models;

namespace FlightPlanner.Core2.Services
{
    public interface IValidator<T> where T : class
    {
        bool IsValid(T request);
    }
}
