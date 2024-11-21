using GourmetGo.Domain.DTOs.usuarios;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Infrastructure.Repositorios
{
    public class UsuariosRepository:IUsuariosRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;


        public UsuariosRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        public async Task<IEnumerable<usuarios>> GetUsuarios()
        {
            try
            {

                var usuarios = await _appDbContext.Usuarios.ToListAsync();

                return usuarios;
                // Devolver la lista de productos en formato JSON con un status 200 OK
            }
            catch (Exception ex)
            {
                // Manejar el error, por ejemplo, registrarlo y/o lanzar una excepción personalizada
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al obtener los USUARIOS.", ex);
            }
        }
        public async Task<usuarios> GetUsuarioByIdAsync(int id)
        {
            return await _appDbContext.Usuarios.FindAsync(id);
        }
        public async Task AddUsuarioAsync(usuarios usuario)
        {
            await _appDbContext.Usuarios.AddAsync(usuario);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<bool> UserExists(string username)
        {
            return await _appDbContext.Usuarios.AnyAsync(u => u.username == username);
        }
        public async Task<usuarios> GetUsuariosByUsernameAsync(string username)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync( u => u.username == username);
        }
        public async Task<bool> RegistarUsuariosAsync(usuarios usuarios)
        {
            if (await UserExists(usuarios.username))
                return false; // Usuario ya existe

            // Hashear la contraseña
            usuarios.password = BCrypt.Net.BCrypt.HashPassword(usuarios.password);

            await AddUsuarioAsync(usuarios);
            return true;
        }
        public async Task<AuthResponseDto> AuthJWT(UsuarioLoginDto usuarioLogin)
        {
            try
            {
                // Verificar si el usuario existe en la base de datos
                var userdb = await GetUsuariosByUsernameAsync(usuarioLogin.username);

                if (userdb == null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "El usuario no existe."
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(usuarioLogin.password, userdb.password))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Contraseña incorrecta."
                    };
                }

                // Generar el token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userdb.username),
                    new Claim("rol", userdb.rol),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["JwtSettings:Issuer"],
                    Audience = _configuration["JwtSettings:Audience"]
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new AuthResponseDto
                {
                    Success = true,
                    Token = tokenHandler.WriteToken(token),
                    Message = "Autenticación exitosa."
                };
            }
            catch (KeyNotFoundException ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error durante el proceso de autenticación.", ex);
            }
        }
    }
}

