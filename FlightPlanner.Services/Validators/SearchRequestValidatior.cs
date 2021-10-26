using System;
using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Services;

namespace FlightPlanner.Services.Validators
{
    public class SearchRequestValidatior : IValidator<SearchFlightsRequest>
    {
        public bool IsValid(SearchFlightsRequest request)
        {
            if (String.IsNullOrEmpty(request.From) || String.IsNullOrEmpty(request.To) ||
                String.IsNullOrEmpty(request.DepartureDate)) return false;

            if (request.From == request.To) return false;

            return true;
        }
    }
}
