using System.ComponentModel.DataAnnotations;

namespace SkyShop1.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int Stock { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
