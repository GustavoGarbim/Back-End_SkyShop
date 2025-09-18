using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyShop1.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public int UserId { get; set; } // FK

        [Required]
        public int CartItemId { get; set; } // FK

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new HashSet<CartItem>();
        }
    }
}
