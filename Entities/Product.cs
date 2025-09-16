namespace SkyShop1.Entities
{
    public class Product
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }
    }
}
