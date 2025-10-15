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
    public class TradeController : ControllerBase
    {
        private readonly IGenericRepository<Trade> _repo;

        public TradeController(IGenericRepository<Trade> repo)
        {
            _repo = repo;
        }

        // GET api/trade/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var trade = await _repo.GetByIdAsync(id);
            return trade is null ? NotFound() : Ok(trade.ToDto());
        }

        // GET api/trade
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var trades = await _repo.GetAllAsync();
            var dtos = trades.Select(e => e.ToDto());
            return Ok(dtos);
        }

        // POST api/trade
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TradeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var trade = dto.ToEntity();
            await _repo.AddAsync(trade);

            return CreatedAtAction(nameof(GetById), new { id = trade.Id }, trade.ToDto());
        }

        // PUT api/trade/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] TradeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound("Trade not found");

            dto.ApplyTo(entity);
            await _repo.UpdateAsync(entity);

            return Ok(entity.ToDto());
        }

        // DELETE api/trade/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound("Trade not found");

            await _repo.DeleteAsync(entity.Id);
            return NoContent();
        }
    }
}
