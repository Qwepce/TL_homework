using System.Text.Json.Serialization;

namespace server.Entities
{
    public class Currency
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Symbol { get; set; } = string.Empty;

        [JsonIgnore]
        public decimal MinPrice { get; set; }

        [JsonIgnore]
        public decimal MaxPrice { get; set; }
    }
}
