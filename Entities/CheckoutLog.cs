using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyShop1.Entities
{
    public class CheckoutLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public DateTime LogTimeStamp { get; set; } = DateTime.UtcNow.ToLocalTime();

        [Required]
        public int CheckoutId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        [ForeignKey("CheckoutId")]
        public virtual Checkout? Checkout { get; set; }
    }
}
