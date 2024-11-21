using GourmetGo.Domain.DTOs.usuarios;
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
        Task<bool> adduser(usuarios usuario);
        Task<AuthResponseDto> AuthenticateAsync(UsuarioLoginDto usuarioLogin);
    }
}
