using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;

namespace FlightPlanner.Services.Validators
{
    public class CarrierValidator : IValidator<FlightRequest>
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.Carrier);
        }
    }
}
