using Findexium.Data;
using Findexium.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findexium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public RatingController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRatingDTO()
        {
            return await _context.RatingDTO.ToListAsync();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDTO>> GetRatingDTO(int id)
        {
            var ratingDTO = await _context.RatingDTO.FindAsync(id);

            if (ratingDTO == null)
            {
                return NotFound();
            }

            return ratingDTO;
        }

        // PUT: api/Rating/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatingDTO(int id, RatingDTO ratingDTO)
        {
            if (id != ratingDTO.RatingId)
            {
                return BadRequest();
            }

            _context.Entry(ratingDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingDTOExists(id))
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

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatingDTO>> PostRatingDTO(RatingDTO ratingDTO)
        {
            _context.RatingDTO.Add(ratingDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatingDTO", new { id = ratingDTO.RatingId }, ratingDTO);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatingDTO(int id)
        {
            var ratingDTO = await _context.RatingDTO.FindAsync(id);
            if (ratingDTO == null)
            {
                return NotFound();
            }

            _context.RatingDTO.Remove(ratingDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingDTOExists(int id)
        {
            return _context.RatingDTO.Any(e => e.RatingId == id);
        }
    }
}
