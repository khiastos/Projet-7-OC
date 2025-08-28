using Findexium.Data;
using Findexium.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findexium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public CurvePointController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/CurvePoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurvePointDTO>>> GetCurvePointDTO()
        {
            return await _context.CurvePointDTO.ToListAsync();
        }

        // GET: api/CurvePoint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurvePointDTO>> GetCurvePointDTO(int id)
        {
            var curvePointDTO = await _context.CurvePointDTO.FindAsync(id);

            if (curvePointDTO == null)
            {
                return NotFound();
            }

            return curvePointDTO;
        }

        // PUT: api/CurvePoint/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurvePointDTO(int id, CurvePointDTO curvePointDTO)
        {
            if (id != curvePointDTO.CurvePointId)
            {
                return BadRequest();
            }

            _context.Entry(curvePointDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurvePointDTOExists(id))
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

        // POST: api/CurvePoint
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurvePointDTO>> PostCurvePointDTO(CurvePointDTO curvePointDTO)
        {
            _context.CurvePointDTO.Add(curvePointDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurvePointDTO", new { id = curvePointDTO.CurvePointId }, curvePointDTO);
        }

        // DELETE: api/CurvePoint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePointDTO(int id)
        {
            var curvePointDTO = await _context.CurvePointDTO.FindAsync(id);
            if (curvePointDTO == null)
            {
                return NotFound();
            }

            _context.CurvePointDTO.Remove(curvePointDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurvePointDTOExists(int id)
        {
            return _context.CurvePointDTO.Any(e => e.CurvePointId == id);
        }
    }
}
