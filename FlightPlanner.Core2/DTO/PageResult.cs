using System.Collections.Generic;
using FlightPlanner.Core2.Models;

namespace FlightPlanner.Core2.DTO
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }

        public PageResult()
        {
            Items = new List<Flight>();
        }
    }
}
