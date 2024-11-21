using GourmetGo.Domain.DTOs.mesas;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GourmetGo.Infrastructure.Repositorios
{
    public class MesaRepository : IMesaRepository
    {
        private readonly AppDbContext _context;

        public MesaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Agregar una nueva mesa
        public async Task<mesas> AddMesasAsync(mesas mesas)
        {
            _context.Mesas.Add(mesas);
            await _context.SaveChangesAsync();
            return mesas;
        }

        // Obtener todas las mesas
        public async Task<IEnumerable<mesas>> GetAllAsync()
        {
            return await _context.Mesas.ToListAsync();
        }

        // Obtener una mesa por su ID
        public async Task<mesas> ObtenerMesaPorIdAsync(int idMesa)
        {
            return await VerificarMesaExiste(idMesa);
        }

        // Cambiar el estado de una mesa
        public async Task CambiarEstadoMesaAsync(int idMesa, bool nuevoEstado)
        {
            var mesa = await VerificarMesaExiste(idMesa);

            mesa.estado = nuevoEstado; // Cambia el estado de la mesa
            _context.Entry(mesa).Property(m => m.estado).IsModified = true;
            await _context.SaveChangesAsync();
        }

        // Verificar si una mesa está ocupada
        public async Task<bool> EsMesaOcupadaAsync(int idMesa)
        {
            var mesa = await VerificarMesaExiste(idMesa);
            return mesa.estado; // Retorna true si está ocupada, false si está libre
        }

        // Método privado para validar la existencia de una mesa
        public async Task<mesas> VerificarMesaExiste(int idMesa)
        {
            var mesa = await _context.Mesas.FindAsync(idMesa);
            if (mesa == null)
            {
                throw new InvalidOperationException("La mesa no existe.");
            }
            return mesa;
        }
    }
}

