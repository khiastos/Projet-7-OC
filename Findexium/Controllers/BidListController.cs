using Findexium.Data;
using Findexium.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findexium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidListController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public BidListController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/BidList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidListDTO>>> GetAll()
        {
            return await _context.BidListDTO.ToListAsync();
        }

        // GET: api/BidList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BidListDTO>> GetById(int id)
        {
            var bidListDTO = await _context.BidListDTO.FindAsync(id);

            if (bidListDTO == null)
            {
                return NotFound();
            }

            return bidListDTO;
        }

        // PUT: api/BidList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidListDTO(int id, BidListDTO bidListDTO)
        {
            if (id != bidListDTO.BidListId)
            {
                return BadRequest();
            }

            _context.Entry(bidListDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidListDTOExists(id))
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

        // POST: api/BidList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BidListDTO>> PostBidListDTO(BidListDTO bidListDTO)
        {
            _context.BidListDTO.Add(bidListDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBidListDTO", new { id = bidListDTO.BidListId }, bidListDTO);
        }

        // DELETE: api/BidList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidListDTO(int id)
        {
            var bidListDTO = await _context.BidListDTO.FindAsync(id);
            if (bidListDTO == null)
            {
                return NotFound();
            }

            _context.BidListDTO.Remove(bidListDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidListDTOExists(int id)
        {
            return _context.BidListDTO.Any(e => e.BidListId == id);
        }
    }
}
