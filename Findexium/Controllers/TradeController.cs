using Findexium.Data;
using Findexium.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findexium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public TradeController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/Trade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TradeDTO>>> GetTradeDTO()
        {
            return await _context.TradeDTO.ToListAsync();
        }

        // GET: api/Trade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TradeDTO>> GetTradeDTO(int id)
        {
            var tradeDTO = await _context.TradeDTO.FindAsync(id);

            if (tradeDTO == null)
            {
                return NotFound();
            }

            return tradeDTO;
        }

        // PUT: api/Trade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTradeDTO(int id, TradeDTO tradeDTO)
        {
            if (id != tradeDTO.TradeId)
            {
                return BadRequest();
            }

            _context.Entry(tradeDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeDTOExists(id))
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

        // POST: api/Trade
        [HttpPost]
        public async Task<ActionResult<TradeDTO>> PostTradeDTO(TradeDTO tradeDTO)
        {
            _context.TradeDTO.Add(tradeDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTradeDTO", new { id = tradeDTO.TradeId }, tradeDTO);
        }

        // DELETE: api/Trade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTradeDTO(int id)
        {
            var tradeDTO = await _context.TradeDTO.FindAsync(id);
            if (tradeDTO == null)
            {
                return NotFound();
            }

            _context.TradeDTO.Remove(tradeDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TradeDTOExists(int id)
        {
            return _context.TradeDTO.Any(e => e.TradeId == id);
        }
    }
}
