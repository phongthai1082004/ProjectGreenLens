using Microsoft.AspNetCore.Mvc;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase where T : BaseEntity, new()
    {

        protected readonly IBaseService<T> _service;

        public BaseController(IBaseService<T> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetById(int id)
        {
            try
            {
                var entity = await _service.getByIdAsync(id);
                return Ok(entity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var entities = await _service.getAllAsync();
            return Ok(entities);
        }
        [HttpPost]
        public async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");
            var createdEntity = await _service.createAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.id }, createdEntity);
        }
        [HttpPut]
        public async Task<ActionResult<T>> Update([FromBody] T entity)
        {
            if (entity == null || entity.id <= 0)
                return BadRequest("Invalid entity or ID.");
            try
            {
                var updatedEntity = await _service.updateAsync(entity);
                return Ok(updatedEntity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.deleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
