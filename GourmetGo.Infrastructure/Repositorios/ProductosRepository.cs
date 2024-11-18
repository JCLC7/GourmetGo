using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Infrastructure.Repositorios
{
    public class ProductosRepository : IProductosRepository
    {
       private readonly AppDbContext _appDbContext;

        public ProductosRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            try
            {
           
                var productos = await _appDbContext.Productos.ToListAsync();
             
                return productos;
                // Devolver la lista de productos en formato JSON con un status 200 OK
            }
            catch (Exception ex)
            {
                // Manejar el error, por ejemplo, registrarlo y/o lanzar una excepción personalizada
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al obtener los productos.", ex);
            }
        }
    



    public async Task<Producto> GetByIdAsync(int id)
        {
            return await _appDbContext.Productos.FindAsync(id);
        }

        public async Task AddAsync(Producto producto)
        {
            await _appDbContext.Productos.AddAsync(producto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {
            _appDbContext.Productos.Update(producto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await GetByIdAsync(id);
            _appDbContext.Productos.Remove(producto);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
