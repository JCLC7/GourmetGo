using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GourmetGo.Application.DTOs;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace GourmetGo.Application.Servicios
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuariosRepository _repository;
        private readonly IConfiguration _configuration;

        public UsuariosServices(IUsuariosRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public async Task<IEnumerable<usuarios>> GetUsuarios() => await _repository.GetUsuarios();
        public async Task<bool> RegistrarUsuarioAsync(usuarios usuario)
        {
            if (await _repository.UserExists(usuario.username))
                return false; // Usuario ya existe

            // Hashear la contraseña
            usuario.password = BCrypt.Net.BCrypt.HashPassword(usuario.password);

            await _repository.AddUsuarioAsync(usuario);
            return true;
        }

        public async Task<string> AuthenticateAsync(UsuarioLoginDto usuarioLogin)
        {
            // Verificar si el usuario existe en la base de datos
            var usuario = await _repository.UserExists(usuarioLogin.username);
            var userdb = await _repository.GetUsuariosByUsernameAsync(usuarioLogin.username);

            if (usuario==false)
            {
                return "usuario no existe"; // el usuario no existe
            }else if(!BCrypt.Net.BCrypt.Verify(usuarioLogin.password, userdb.password))
            {
                    return "Contraseña incorrecta"; // La contraseña no coincide
                
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
            return tokenHandler.WriteToken(token);
        }

    }
}
