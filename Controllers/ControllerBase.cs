using Microsoft.AspNetCore.Mvc;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TResponseDto, TAddDto, TUpdateDto> : ControllerBase
    {
        protected readonly IBaseService<TEntity, TResponseDto, TAddDto, TUpdateDto> _service;

        public BaseController(IBaseService<TEntity, TResponseDto, TAddDto, TUpdateDto> service)
        {
            _service = service;
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result); // ApiResponseMiddleware sẽ wrap
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        // POST: api/[controller]
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TAddDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        // PUT: api/[controller]
        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TUpdateDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        // DELETE: api/[controller]/{id}
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
