namespace SkyShop1.Entities
{
    public class CartItem
    {
        public int Id { get; set; } // PK
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
