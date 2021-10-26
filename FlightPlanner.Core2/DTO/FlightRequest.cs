namespace FlightPlanner.Core2.DTO
{
    public class FlightRequest
    {
        public AirportRequest To { get; set; }
        public AirportRequest From { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
