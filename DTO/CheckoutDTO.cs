using SkyShop1.Entities;

namespace SkyShop1.DTO
{
    public class CreateCheckoutDTO
    {
        public int CartId { get; set; }

        // Address to delivery
        public string Address { get; set; }
    }
}
