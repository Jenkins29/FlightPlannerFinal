using System;

namespace FlightPlannerFinal.Models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
        public static bool SearchRequestValidation(SearchFlightsRequest req)
        {
            if (String.IsNullOrEmpty(req.From) || String.IsNullOrEmpty(req.To) ||
                String.IsNullOrEmpty(req.DepartureDate)) return false;

            if (req.From == req.To) return false;

            return true;
        }
    }
}