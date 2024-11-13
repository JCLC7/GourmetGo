using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controladores
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ProductoController : ControllerBase
        {
            private readonly IProductosservices _service;

            public ProductoController(IProductosservices service)
            {
                _service = service;
            }


            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var productos = await _service.GetAllAsync();
                return Ok(productos);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var producto = await _service.GetByIdAsync(id);
                return producto == null ? NotFound() : Ok(producto);
            }

            [HttpPost]
            public async Task<IActionResult> Add(Producto producto)
            {
                await _service.AddAsync(producto);
                return CreatedAtAction(nameof(GetById), new { id = producto.id_producto }, producto);
            }

            [HttpPut]
            public async Task<IActionResult> Update(Producto producto)
            {
                await _service.UpdateAsync(producto);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
        }
    }


