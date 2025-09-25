using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SkyShop1.Data;
using SkyShop1.DTO;
using SkyShop1.Entities;
using System.Security.Claims;

namespace SkyShop1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserDTO>> PutUser(int id, [FromBody] UpdateUserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuario não encontrado.");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Address = userDto.Address;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //PUT: api/User/password/5
        [Authorize]
        [HttpPut("password/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDTO passwordDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user.Id.ToString() != userIdFromToken)
            {
                return Forbid();
            }

            if (passwordDto.OldPassword != user.Password)
            {
                return BadRequest("A senha antiga está incorreta.");
            }

            var newPassword = passwordDto.NewPassword;

            user.Password = newPassword;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Ocorreu um erro ao salvar a nova senha.");
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> PostUser(CreateUserDTO userDTO)
        {
            var user = await _context.Users.AnyAsync(m => m.Email == userDTO.Email);

            if (user) { return Conflict("Este e-mail já está em uso."); }

            var newUser = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                CreatedAt = DateTime.UtcNow,
                Address = userDTO.Address
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
