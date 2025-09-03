using Findexium.Domain;
using Findexium.DTOs;
using Findexium.Mappers;
using Findexium.Repositories.Interfaces;
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
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity.ToDto());
        }

        // POST api/bidlist
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BidListDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = dto.ToEntity();
            await _repo.AddAsync(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.BidListId }, entity.ToDto());
        }

        // PUT api/bidlist/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BidListDTO dto)
        {
            if (id != dto.BidListId) return BadRequest("Id mismatch.");

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

        // DELETE api/bidlist/5
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
