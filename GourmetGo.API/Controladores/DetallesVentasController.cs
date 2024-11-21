using GourmetGo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesVentasController :ControllerBase
    {
        private readonly IDetalleVentaService _detalle;
        public DetallesVentasController(IDetalleVentaService detalle)
        {
            _detalle = detalle;
        }

        [HttpGet("venta/{idVenta}/productos")]
        public async Task<IActionResult> GetDetallesConTotalByVentaId(int idVenta)
        {
            var detallesConTotal = await _detalle.GetDetallesConTotalByVentaIdAsync(idVenta);
            if (detallesConTotal == null)
                return NotFound(new { error = "No se encontraron detalles para esta venta." });

            return Ok(detallesConTotal);
        }


    }
}
