using SkyShop1.Entities;

namespace SkyShop1.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public static UserDTO FromEntity(User user) => new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Address = user.Address
        };
    }
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }

    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}