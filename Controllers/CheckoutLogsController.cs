using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyShop1.Data;
using SkyShop1.DTO;
using SkyShop1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyShop1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CheckoutLogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CheckoutLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckoutLogDTO>>> GetLogsForCheckout([FromQuery] int checkoutId)
        {
            var checkoutExists = await _context.Checkouts.AnyAsync(c => c.Id == checkoutId);
            if (!checkoutExists)
            {
                return NotFound($"Nenhum checkout encontrado com o id: {checkoutId}");
            }

            var logs = await _context.CheckoutLogs
                .Where(log => log.CheckoutId == checkoutId)
                .OrderByDescending(log => log.LogTimeStamp)
                .Select(log => new CheckoutLogDTO
            {
                    Id = log.Id,
                    CheckoutId = log.CheckoutId,
                    UserId = log.UserId,
                    LogTimeStamp = log.LogTimeStamp
            })
                .ToListAsync();

            return Ok(logs);
        }

        // GET: api/CheckoutLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckoutLog>> GetCheckoutLog(int id)
        {
            var checkoutLog = await _context.CheckoutLogs.FindAsync(id);

            if (checkoutLog == null)
            {
                return NotFound();
            }

            return checkoutLog;
        }

        // PUT: api/CheckoutLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckoutLog(int id, CheckoutLog checkoutLog)
        {
            if (id != checkoutLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkoutLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckoutLogExists(id))
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

        // POST: api/CheckoutLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckoutLog>> PostCheckoutLog(CheckoutLog checkoutLog)
        {
            _context.CheckoutLogs.Add(checkoutLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckoutLog", new { id = checkoutLog.Id }, checkoutLog);
        }

        // DELETE: api/CheckoutLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckoutLog(int id)
        {
            var checkoutLog = await _context.CheckoutLogs.FindAsync(id);
            if (checkoutLog == null)
            {
                return NotFound();
            }

            _context.CheckoutLogs.Remove(checkoutLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckoutLogExists(int id)
        {
            return _context.CheckoutLogs.Any(e => e.Id == id);
        }
    }
}
