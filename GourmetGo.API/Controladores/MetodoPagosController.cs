using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs.metodospago;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _service;

        public MetodosPagoController(IMetodoPagoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var metodos = await _service.GetAllAsync();
            return Ok(metodos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var metodo = await _service.GetByIdAsync(id);
            if (metodo == null)
                return NotFound();

            return Ok(metodo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MetodoPagoDto metodoPagoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.CreateAsync(metodoPagoDto);
            return CreatedAtAction(nameof(GetById), new { id }, metodoPagoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MetodoPagoDto metodoPagoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, metodoPagoDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

}
