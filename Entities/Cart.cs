namespace SkyShop1.Entities
{
    public class Cart
    {
        public int Id { get; set; } // PK
        public int UserId { get; set; } // FK
        public int CartItemId { get; set; } // FK

        public virtual ICollection<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new HashSet<CartItem>();
        }
    }
}
