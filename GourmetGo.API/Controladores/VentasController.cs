using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Servicios;
using GourmetGo.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controladores
{
    using GourmetGo.Domain.DTOs.productos;
    using GourmetGo.Domain.DTOs.ventas;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentasServices _ventaService;

        public VentasController(IVentasServices ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = await _ventaService.GetAllVentasAsync();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venta = await _ventaService.GetVentaByIdAsync(id);
            if (venta == null) return NotFound();
            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VentaCreateDto venta)
        {
            var newId = await _ventaService.CreateVentaAsync(venta);
            return CreatedAtAction(nameof(GetById), new { id = newId }, venta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VentaUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { error = "Datos inválidos para la actualización." });
            }

            try
            {
                var updated = await _ventaService.UpdateVentaAsync(id, dto);
                if (!updated)
                {
                    return NotFound(new { error = "La venta especificada no existe." });
                }

                return Ok(new {succes = "venta actualizada"}); // Actualización exitosa
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ventaService.DeleteVentaAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpPost("abrir-mesa/{idMesa}")]
        public async Task<IActionResult> AbrirMesa(int idMesa)
        {
            var idVenta = await _ventaService.AbrirMesaAsync(idMesa);
            return Ok(new { IdVenta = idVenta });
        }

        [HttpPost("agregar-producto")]
        public async Task<IActionResult> AgregarProducto([FromBody] AgregarProductoDto dto)
        {
            try
            {
                await _ventaService.AgregarProductoADetalleAsync(dto);
                return Ok(new { message = "Producto agregado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

}
