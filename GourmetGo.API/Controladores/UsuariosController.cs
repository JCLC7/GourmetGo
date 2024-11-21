
using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Servicios;
using GourmetGo.Domain.DTOs.usuarios;
using GourmetGo.Domain.Entidades;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GourmetGo.API.Controladores
{
    [ApiController]
    [Route("api/usuarios/[controller]")]

    public class UsuariosController : ControllerBase
    {
   
        private readonly IUsuariosServices _services;
        public UsuariosController(AppDbContext context, IUsuariosServices uservices)
        {
          
            _services = uservices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuarios>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _services.GetUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los usuarios.", Details = ex.Message });
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> adduser([FromBody] usuarios usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isRegistered = await _services.adduser(usuario);

            if (!isRegistered)
                return Conflict(new { Message = "El nombre de usuario ya está en uso." });

            return Ok(new { Message = "Usuario registrado exitosamente." });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var authResponse = await _services.AuthenticateAsync(usuarioLogin);
                if (authResponse == null || !authResponse.Success)
                {
                    return Conflict(new { Message = authResponse?.Message ?? "Credenciales inválidas" });
                }

                return Ok(new { Token = authResponse.Token, Message = authResponse.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error durante la autenticación.", Details = ex.Message });
            }
        }

    }
}
