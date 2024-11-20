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

using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using GourmetGo.Domain.DTOs;


namespace GourmetGo.Application.Servicios
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuariosRepository _repository;
        

        public UsuariosServices(IUsuariosRepository repository)
        {
            _repository = repository;
           
        }
        public async Task<IEnumerable<usuarios>> GetUsuarios() => await _repository.GetUsuarios();
        public async Task<bool> adduser(usuarios usuario) => await _repository.RegistarUsuariosAsync(usuario);
       
        public async Task<string> AuthenticateAsync(UsuarioLoginDto usuarioLogin) => await _repository.AuthJWT(usuarioLogin);
        
           

    }
}
