namespace SkyShop1.Entities
{
    public class Checkout
    {
        public int Id { get; set; } // PK
        public int CartId { get; set; } // FK
        public int UserId { get; set; } // FK
        public int TotalItems { get; set; }
        public float TotalValue { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPayed { get; set; }
    }
}
