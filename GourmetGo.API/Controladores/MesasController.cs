using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MesasController : ControllerBase
    {
        private readonly IMesaServices services;
        public  MesasController(IMesaServices mesa) {
            services = mesa;    
        
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mesas = await services.GetAllTables();
            return new OkObjectResult(mesas);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDispo(int id, [FromBody] MesaDTO mesaDto)
        {

            if (await services.UpdateDisponibilidad(id, mesaDto)) return Ok(new { Message = "mesa actualizada exitosamente." }); ;
            return NotFound(new { Message = "mesa no encontrada" }); ;
        }


    }

 }

