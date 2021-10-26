using System;
using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;

namespace FlightPlanner.Services.Validators
{
    public class TimeFrameValidator : IValidator<FlightRequest>
    {
        public bool IsValid(FlightRequest request)
        {
            try
            {
                return DateTime.Parse(request.ArrivalTime) > DateTime.Parse(request.DepartureTime);
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
