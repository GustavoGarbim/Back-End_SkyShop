namespace SkyShop1.DTO
{
    public class CheckoutLogDTO
    {
        public int Id { get; set; }
        public int CheckoutId { get; set; }
        public int UserId { get; set; }
        public DateTime LogTimeStamp { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}