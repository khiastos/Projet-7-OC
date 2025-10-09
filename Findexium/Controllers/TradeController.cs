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
            var trade = await _repo.GetByIdAsync(id);
            if (trade is null) return NotFound("Trade not found");

            dto.ApplyTo(trade);
            await _repo.UpdateAsync(trade);

            return Ok(new
            {
                Message = "Updated successfully",
                Data = trade.ToDto()
            });
        }

        // DELETE api/trade/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var trade = await _repo.GetByIdAsync(id);
            if (trade is null) return NotFound();

            await _repo.DeleteAsync(id);

            return Ok(new
            {
                Message = "Deleted successfully",
                DeletedId = id
            });
        }
    }
}
