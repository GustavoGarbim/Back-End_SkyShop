using System.ComponentModel.DataAnnotations;

namespace SkyShop1.DTO
{
    public class AddOrUpdateCartItemDTO
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "A quantidade deve ser de no mínimo 1.")]
        public int Quantity { get; set; }
    }

    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CartDTO
    {
        public int Id { get; set; }
        public List<CartItemDTO> Items { get; set; } = new();
        public decimal TotalValue { get; set; }
    }
}
