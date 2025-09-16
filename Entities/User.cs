namespace SkyShop1.Entities
{
    public class User
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
    }
}
