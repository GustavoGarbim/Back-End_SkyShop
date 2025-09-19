using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyShop1.Data;
using SkyShop1.Entities;
using SkyShop1.DTO;

using Microsoft.AspNetCore.Authorization;

namespace SkyShop1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartsController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/cart/items
        [HttpPost("items")]
        public async Task<ActionResult<CartDTO>> AddItemToCart([FromBody] AddOrUpdateCartItemDTO itemDTO, [FromQuery] int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists) {
                return NotFound("Usuário não encontrado.");
            }

            var product = await _context.Products.FindAsync(itemDTO.ProductId);
            if (product == null) {
                return NotFound("Produto não encontrado.");
            }

            var cart = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == itemDTO.ProductId);
            if (existingItem != null) {
                existingItem.Quantity += itemDTO.Quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity
                };
                cart.Items.Add(newItem);
            }

            await _context.SaveChangesAsync();

            return await GetCartByUserId(userId);
        }


        // GET: api/Carts?5
        [HttpGet]
        public async Task<ActionResult<CartDTO>> GetCart([FromQuery] int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound("Usuário não encontrado.");
            }

            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return Ok(new Cart());
            }

            var cartDTO = new CartDTO
            {
                Id = cart.Id,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "Produto removido",
                    Price = i.Product?.Price ?? 0,
                    Quantity = i.Quantity
                }).ToList(),
            };

            cartDTO.TotalValue = cartDTO.Items.Sum(i => i.Price * i.Quantity);

            return Ok(cartDTO);
        }

        private async Task<ActionResult<CartDTO>> GetCartByUserId(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                {
                return Ok(new CartDTO());
            }

            var cartDto = new CartDTO
            {
                Id = cart.Id,
                Items = cart.Items.Select(item => new CartItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "Produto Indisponível",
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
            cartDto.TotalValue = cartDto.Items.Sum(item => item.Price * item.Quantity);

            return Ok(cartDto);
        }

        // PUT: api/carts/items/{productId}?userId=1
        [HttpPut("items/{productId}")]
        public async Task<ActionResult<CartDTO>> UpdateItemQuantity(int productId, [FromBody] AddOrUpdateCartItemDTO quantityDto, [FromQuery] int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!userExists)
            {
                return NotFound("Usuário não encontrado.");
            }

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            var itemToUpdate = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (itemToUpdate == null)
            {
                return NotFound("Item não encontrado no carrinho.");
            }

            itemToUpdate.Quantity = quantityDto.Quantity;
            await _context.SaveChangesAsync();
            return await GetCartByUserId(userId);
        }

        // DELETE: /api/carts/items/{productId}?userId=1
        [HttpDelete("items/{productId}")]
        public async Task<ActionResult<CartDTO>> RemoveItemFromCart(int productId, [FromQuery] int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null) return NotFound("Carrinho não encontrado.");

            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove == null) return NotFound("Item não encontrado no carrinho.");

            _context.CartItems.Remove(itemToRemove);
            await _context.SaveChangesAsync();

            return await GetCartByUserId(userId);
        }
    }
}
