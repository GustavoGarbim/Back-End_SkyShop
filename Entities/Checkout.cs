using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyShop1.Entities
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int TotalItems { get; set; }

        [Required]
        public float TotalValue { get; set; }

        [Required]
        public bool? IsDeleted { get; set; }

        [Required]
        public bool? IsPayed { get; set; }
    }
}
