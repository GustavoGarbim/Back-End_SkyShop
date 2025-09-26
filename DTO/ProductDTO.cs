namespace SkyShop1.DTO
{
    public class CreateOrUpdateProductDTO
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }

    public class ReadProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
