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
    public class RatingController : ControllerBase
    {
        private readonly IGenericRepository<Rating> _repo;

        public RatingController(IGenericRepository<Rating> repo)
        {
            _repo = repo;
        }

        // GET api/rating/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var rating = await _repo.GetByIdAsync(id);
            return rating is null ? NotFound() : Ok(rating.ToDto());
        }

        // GET api/rating
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var ratings = await _repo.GetAllAsync();
            var dtos = ratings.Select(e => e.ToDto());
            return Ok(dtos);
        }

        // POST api/rating
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] RatingDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var rating = dto.ToEntity();
            await _repo.AddAsync(rating);

            return CreatedAtAction(nameof(GetById), new { id = rating.Id }, rating.ToDto());
        }

        // PUT api/rating/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] RatingDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound("Rating not found");

            dto.ApplyTo(entity);
            await _repo.UpdateAsync(entity);

            return Ok(entity.ToDto());
        }

        // DELETE api/rating/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return NotFound("Rating not found");

            await _repo.DeleteAsync(entity.Id);
            return NoContent();
        }
    }
}
