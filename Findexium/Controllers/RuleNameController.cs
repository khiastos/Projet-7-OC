using Findexium.Data;
using Findexium.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findexium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleNameController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public RuleNameController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/RuleName
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleNameDTO>>> GetRuleNameDTO()
        {
            return await _context.RuleNameDTO.ToListAsync();
        }

        // GET: api/RuleName/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleNameDTO>> GetRuleNameDTO(int id)
        {
            var ruleNameDTO = await _context.RuleNameDTO.FindAsync(id);

            if (ruleNameDTO == null)
            {
                return NotFound();
            }

            return ruleNameDTO;
        }

        // PUT: api/RuleName/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuleNameDTO(int id, RuleNameDTO ruleNameDTO)
        {
            if (id != ruleNameDTO.RuleNameId)
            {
                return BadRequest();
            }

            _context.Entry(ruleNameDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleNameDTOExists(id))
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

        // POST: api/RuleName
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleNameDTO>> PostRuleNameDTO(RuleNameDTO ruleNameDTO)
        {
            _context.RuleNameDTO.Add(ruleNameDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuleNameDTO", new { id = ruleNameDTO.RuleNameId }, ruleNameDTO);
        }

        // DELETE: api/RuleName/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleNameDTO(int id)
        {
            var ruleNameDTO = await _context.RuleNameDTO.FindAsync(id);
            if (ruleNameDTO == null)
            {
                return NotFound();
            }

            _context.RuleNameDTO.Remove(ruleNameDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuleNameDTOExists(int id)
        {
            return _context.RuleNameDTO.Any(e => e.RuleNameId == id);
        }
    }
}
