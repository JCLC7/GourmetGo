using GourmetGo.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<usuarios>> GetUsuarios();
        Task<usuarios> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(usuarios usuario);
        Task<bool> UserExists(string username);
        Task <usuarios> GetUsuariosByUsernameAsync(string username);
    }
}
