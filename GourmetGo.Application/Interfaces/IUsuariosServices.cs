
using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Interfaces
{
    public interface IUsuariosServices
    {
        Task<IEnumerable<usuarios>> GetUsuarios();
        Task<bool> RegistrarUsuarioAsync(usuarios usuario);
        Task<string> AuthenticateAsync(UsuarioLoginDto usuarioLogin);
    }
}
