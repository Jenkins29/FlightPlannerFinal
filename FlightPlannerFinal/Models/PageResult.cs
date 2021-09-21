using System.Collections.Generic;

namespace FlightPlannerFinal.Models
{
    public class PageResult
    {
        public List<Flight> Items { get; set; } = new List<Flight>();
        public int Page { get; set; }
        public int TotalItems { get; set; }
    }
}