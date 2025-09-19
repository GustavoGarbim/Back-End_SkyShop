using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyShop1.Data;
using SkyShop1.DTO;
using SkyShop1.Entities;

using Microsoft.AspNetCore.Authorization;

namespace SkyShop1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CheckoutsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Checkouts?userId=1
        [HttpPost]
        public async Task<IActionResult> ProcessCheckout([FromBody] CreateCheckoutDTO checkoutDTO, [FromQuery] int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(c => c.Id == checkoutDTO.CartId && c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Carrinho não encontrado ou não pertence a este usuário");
            }
            if (!cart.Items.Any())
            {
                return BadRequest("O carrinho está vazio e não pode ser finalizado.");
            }

            float totalValue = cart.Items.Sum(item => (item.Product?.Price ?? 0) * item.Quantity);
            int totalItems = cart.Items.Sum(item => item.Quantity);

            var newCheckout = new Checkout
            {
                UserId = userId,
                CartId = cart.Id,
                TotalValue = totalValue,
                TotalItems = totalItems,
                IsDeleted = false,
                IsPayed = false

            };

            _context.Checkouts.Add(newCheckout);

            _context.CartItems.RemoveRange(cart.Items);

            await _context.SaveChangesAsync();

            var checkoutLog = new CheckoutLog
            {
                CheckoutId = newCheckout.Id,
                UserId = userId,
                LogTimeStamp = DateTime.UtcNow,
                IsDeleted = true,
            };
            _context.CheckoutLogs.Add(checkoutLog);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Compra finalizada com sucesso!",
                CheckoutId = newCheckout.Id,
                TotalValue = newCheckout.TotalValue
            });
        }
    }
}
