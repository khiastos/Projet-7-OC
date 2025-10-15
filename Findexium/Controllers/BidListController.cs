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
    public class BidListController : ControllerBase
    {
        private readonly IGenericRepository<BidList> _repo;

        public BidListController(IGenericRepository<BidList> repo)
        {
            _repo = repo;
        }

        // GET api/bidlist/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var bidList = await _repo.GetByIdAsync(id);
            return bidList is null ? NotFound() : Ok(bidList.ToDto());
        }

        // GET api/bidlist
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var bidLists = await _repo.GetAllAsync();
            var dtos = bidLists.Select(e => e.ToDto());
            return Ok(dtos);
        }

        // POST api/bidlist
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] BidListDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bidList = dto.ToEntity();
            await _repo.AddAsync(bidList);

            return CreatedAtAction(nameof(GetById), new { id = bidList.Id }, bidList.ToDto());
        }

        // PUT api/bidlist/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] BidListDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound("Bidlist not found");

            dto.ApplyTo(entity);
            await _repo.UpdateAsync(entity);

            return Ok(entity.ToDto());
        }

        // DELETE api/bidlist/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound("Bidlist not found");

            await _repo.DeleteAsync(entity.Id);
            return NoContent();
        }
    }
}
