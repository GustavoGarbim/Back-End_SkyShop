using System.ComponentModel.DataAnnotations;

namespace SkyShop1.DTO
{
    public class AddOrUpdateCartItemDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "A quantidade deve ser de no mínimo 1.")]
        public int Quantity { get; set; }
    }
}
