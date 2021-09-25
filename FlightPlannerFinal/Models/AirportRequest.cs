using System.Text.Json.Serialization;

namespace FlightPlannerFinal.Models
{
    public class AirportRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string Airport { get; set; }
    }
}