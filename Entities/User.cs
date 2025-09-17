using System.ComponentModel.DataAnnotations;

namespace SkyShop1.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
