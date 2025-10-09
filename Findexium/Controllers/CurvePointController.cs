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
    public class CurvePointController : ControllerBase
    {
        private readonly IGenericRepository<CurvePoint> _repo;

        public CurvePointController(IGenericRepository<CurvePoint> repo)
        {
            _repo = repo;
        }

        // GET api/curvepoint/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var curvePoint = await _repo.GetByIdAsync(id);
            return curvePoint is null ? NotFound() : Ok(curvePoint.ToDto());
        }

        // GET api/curvepoint
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var curvePoints = await _repo.GetAllAsync();
            var dtos = curvePoints.Select(e => e.ToDto());
            return Ok(dtos);
        }

        // POST api/curvepoint
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CurvePointDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var curvePoint = dto.ToEntity();
            await _repo.AddAsync(curvePoint);

            return CreatedAtAction(nameof(GetById), new { id = curvePoint.Id }, curvePoint.ToDto());
        }

        // PUT api/curvepoint/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePointDTO dto)
        {
            var curvePoint = await _repo.GetByIdAsync(id);
            if (curvePoint is null) return NotFound("CurvePoint not found");

            dto.ApplyTo(curvePoint);
            await _repo.UpdateAsync(curvePoint);

            return Ok(new
            {
                Message = "Updated successfully",
                Data = curvePoint.ToDto()
            });
        }

        // DELETE api/curvepoint/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var curvePoint = await _repo.GetByIdAsync(id);
            if (curvePoint is null) return NotFound();

            await _repo.DeleteAsync(id);

            return Ok(new
            {
                Message = "Deleted successfully",
                DeletedId = id
            });
        }
    }
}
