using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs.mesas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GourmetGo.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesasController : ControllerBase
    {
        private readonly IMesaServices _mesaServices;

        public MesasController(IMesaServices mesaServices)
        {
            _mesaServices = mesaServices;
        }

        // Agregar una nueva mesa
        [HttpPost]
        public async Task<IActionResult> AddMesa([FromBody] MesaDTO mesaDto)
        {
            if (mesaDto == null)
            {
                return BadRequest(new { Message = "Los datos de la mesa son obligatorios." });
            }

            try
            {
                var nuevaMesa = await _mesaServices.AddMesaAsync(mesaDto);
                return Created(string.Empty, nuevaMesa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al agregar la mesa: {ex.Message}" });
            }
        }

        // Obtener todas las mesas
        [HttpGet]
        public async Task<IActionResult> GetAllMesas()
        {
            try
            {
                var mesas = await _mesaServices.GetAllMesasAsync();
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al obtener las mesas: {ex.Message}" });
            }
        }

        // Actualizar el estado de una mesa
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateMesaEstado(int id, [FromBody] MesaDTO mesaDto)
        {
            if (mesaDto == null)
            {
                return BadRequest(new { Message = "Los datos de la mesa son obligatorios." });
            }

            try
            {
                await _mesaServices.UpdateMesaEstadoAsync(id, mesaDto);
                return Ok(new { Message = "Estado de la mesa actualizado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al actualizar el estado de la mesa: {ex.Message}" });
            }
        }
    }
}

