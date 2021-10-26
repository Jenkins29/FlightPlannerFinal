using System;

namespace FlightPlanner.Core2.DTO
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
