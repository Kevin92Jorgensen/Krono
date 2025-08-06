namespace Krono.Infrastructure.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Gtid { get; set; } = "";
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Description { get; set; } = "";
        public string ProductType { get; set; } = "";
        public string UnitsOfMeasure { get; set; } = "";
        public int IsEU { get; set; }
        public int IsOrganic { get; set; }
        public int Favorite { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PriceEntry> PriceEntries { get; set; } = new List<PriceEntry>();

    }
}
