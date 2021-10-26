using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;

namespace FlightPlanner.Services.Validators
{
    public class AirportCodesEqualityValidator : IValidator<FlightRequest>
    {
        public bool IsValid(FlightRequest request)
        {
           return request?.From?.Airport?.Trim().ToLower() != request?.To?.Airport?.Trim().ToLower();
        }
    }
}
