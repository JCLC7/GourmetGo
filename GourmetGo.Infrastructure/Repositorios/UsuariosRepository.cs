using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Infrastructure.Repositorios
{
    public class UsuariosRepository:IUsuariosRepository
    {
        private readonly AppDbContext _appDbContext;

        public UsuariosRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
    }
}
