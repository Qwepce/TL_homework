namespace server.Entities
{
    public class CurrencyPrice
    {
        public int Id { get; set; }

        public string CurrencyCode { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public Currency Currency { get; set; } = new Currency();
    }
}
