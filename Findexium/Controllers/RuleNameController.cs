using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Mappers;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Findexium.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly IGenericRepository<RuleName> _repo;

        public RuleNameController(IGenericRepository<RuleName> repo)
        {
            _repo = repo;
        }

        // GET api/rulename/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity.ToDto());
        }

        // POST api/rulename
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RuleNameDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = dto.ToEntity();
            await _repo.AddAsync(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.RuleNameId }, entity.ToDto());
        }

        // PUT api/rulename/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RuleNameDTO dto)
        {
            if (id != dto.RuleNameId) return BadRequest("Id mismatch.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound();

            var updated = dto.ToEntity();
            await _repo.UpdateAsync(updated);

            return Ok(new
            {
                Message = "Updated successfully",
                Data = updated.ToDto()
            });
        }

        // DELETE api/rulename/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound();

            await _repo.DeleteAsync(id);

            return Ok(new
            {
                Message = "Deleted successfully",
                DeletedId = id
            });
        }
    }
}
