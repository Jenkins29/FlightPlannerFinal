using System.Collections.Generic;

namespace FlightPlannerFinal.Models
{
    public class PageResult
    {
        public List<Flight> Items { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }

        public PageResult()
        {
            Items = new List<Flight>();
        }
    }
}