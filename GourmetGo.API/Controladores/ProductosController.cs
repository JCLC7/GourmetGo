using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controladores
{
    
        [ApiController]
        [Route("api/productos/[controller]")]
        [Authorize]
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
                return new OkObjectResult(productos);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var producto = await _service.GetByIdAsync(id);
                return producto == null ? NotFound() : Ok(producto);
            }

            [HttpPost]
          
            public async Task<IActionResult> AddAsync([FromBody] Producto producto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _service.AddAsync(producto);
                return Ok(new { Message = "Producto agregado exitosamente." });
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


