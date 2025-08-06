using System.Text.Json.Serialization;

namespace Krono.Infrastructure.Models
{
    public class PriceEntry
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Gtid { get; set; } = "";

        // Relationships
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; } = null!;

        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
