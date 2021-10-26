using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;

namespace FlightPlanner.Services.Validators
{
    public class DepartureTimeValidator : IValidator<FlightRequest>
    {
        public bool IsValid(FlightRequest request)
        {
           return !string.IsNullOrEmpty(request.DepartureTime);
        }
    }
}
