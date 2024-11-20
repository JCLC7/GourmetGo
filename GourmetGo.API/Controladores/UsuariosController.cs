
using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Servicios;
using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GourmetGo.API.Controladores
{
    [ApiController]
    [Route("api/usuarios/[controller]")]

    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUsuariosServices _services;
        public UsuariosController(AppDbContext context, IUsuariosServices uservices)
        {
            _context = context;
            _services = uservices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
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
            var token = await _services.AuthenticateAsync(usuarioLogin);
            if (token == null)
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }

            return Ok(new { Token = token });
        }

    }
}
