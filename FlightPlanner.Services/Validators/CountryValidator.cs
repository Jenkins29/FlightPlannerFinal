using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;

namespace FlightPlanner.Services.Validators
{
    public class CountryValidator : IValidator<FlightRequest>
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To?.Country) && !string.IsNullOrEmpty(request?.From?.Country);
        }
    }
}
