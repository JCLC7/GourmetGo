﻿
using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<usuarios>> GetUsuarios();
        Task<usuarios> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(usuarios usuario);
        Task<bool> UserExists(string username);
        Task <usuarios> GetUsuariosByUsernameAsync(string username);
        Task<bool> RegistarUsuariosAsync(usuarios usuarios);
        Task<string> AuthJWT(UsuarioLoginDto usuarioLogin);
    }
}