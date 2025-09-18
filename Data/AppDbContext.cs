using Microsoft.EntityFrameworkCore;
using SkyShop1.Entities;
using SkyShop1.DTO;

namespace SkyShop1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Checkout> Checkouts { get; set; } = null!;
        public DbSet<CheckoutLog> CheckoutLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkout>()
                .HasOne(checkout => checkout.User)
                .WithMany()
                .HasForeignKey(checkout => checkout.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Checkout>()
                .HasOne(checkout => checkout.Cart)
                .WithMany()
                .HasForeignKey(checkout => checkout.CartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<SkyShop1.DTO.CheckoutLogDTO> CheckoutLogDTO { get; set; }
    }
}
