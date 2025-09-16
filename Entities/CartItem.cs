using System.ComponentModel.DataAnnotations.Schema;

namespace SkyShop1.Entities
{
    public class CartItem
    {
        public int Id { get; set; } // PK
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; } // Why virtual?

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } // Why virtual?
        public int Quantity { get; internal set; }
    }
}
