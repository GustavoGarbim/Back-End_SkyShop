using SkyShop1.Entities;

namespace SkyShop1.DTO
{
    public class CreateCheckoutDTO
    {
        public int CartId { get; set; }
    }

    public class LogCheckoutDTO
    {
        public int CartId { get; set; }
        public UserDTO User { get; set; }
    }
}
