using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Mappers;
using Findexium.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var ruleName = await _repo.GetByIdAsync(id);
            return ruleName is null ? NotFound() : Ok(ruleName.ToDto());
        }

        // GET api/rulename
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var ruleNames = await _repo.GetAllAsync();
            var dtos = ruleNames.Select(e => e.ToDto());
            return Ok(dtos);
        }

        // POST api/rulename
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] RuleNameDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ruleName = dto.ToEntity();
            await _repo.AddAsync(ruleName);

            return CreatedAtAction(nameof(GetById), new { id = ruleName.Id }, ruleName.ToDto());
        }

        // PUT api/rulename/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] RuleNameDTO dto)
        {
            var ruleName = await _repo.GetByIdAsync(id);
            if (ruleName is null) return NotFound("RuleName not found");

            dto.ApplyTo(ruleName);
            await _repo.UpdateAsync(ruleName);

            return Ok(new
            {
                Message = "Updated successfully",
                Data = ruleName.ToDto()
            });
        }

        // DELETE api/rulename/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ruleName = await _repo.GetByIdAsync(id);
            if (ruleName is null) return NotFound();

            await _repo.DeleteAsync(id);

            return Ok(new
            {
                Message = "Deleted successfully",
                DeletedId = id
            });
        }
    }
}
