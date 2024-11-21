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
    public class MetodoPagoRepository : IMetodosPagoRepository
    {
        private readonly AppDbContext _context;

        public MetodoPagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<metodos_pago>> GetAllAsync()
        {
            return await _context.metodos_pago.ToListAsync();
        }

        public async Task<metodos_pago> GetByIdAsync(int id)
        {
            return await _context.metodos_pago.FindAsync(id);
        }

        public async Task<int> CreateAsync(metodos_pago metodoPago)
        {
            _context.metodos_pago.Add(metodoPago);
            await _context.SaveChangesAsync();
            return metodoPago.id_metodo;
        }

        public async Task<bool> UpdateAsync(int id, metodos_pago metodoPago)
        {
            var existingMetodo = await _context.metodos_pago.FindAsync(id);
            if (existingMetodo == null)
                return false;

            existingMetodo.metodo = metodoPago.metodo;
            _context.metodos_pago.Update(existingMetodo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var metodoPago = await _context.metodos_pago.FindAsync(id);
            if (metodoPago == null)
                return false;

            _context.metodos_pago.Remove(metodoPago);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

