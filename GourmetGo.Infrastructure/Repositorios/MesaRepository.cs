using GourmetGo.Domain.DTOs;
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
    public class MesaRepository : IMesaRepository
    {
        private readonly AppDbContext _context;

        public MesaRepository(AppDbContext context)
        {
            _context = context;
           
        }
        public async Task<mesas> addMesas(mesas mesas)
        {
            _context.Mesas.Add(mesas);
             await _context.SaveChangesAsync();
            return mesas;
        }

        public async Task<IEnumerable<mesas>> GetAllAsync()
        {
           return await _context.Mesas.ToListAsync();
        }

        public async Task<bool> updateDispo(int id, MesaDTO mesaDTO)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null) return false;

            mesa.estado = mesaDTO.estado;
            _context.Update(mesa);
            
            return await _context.SaveChangesAsync() >0 ;

        }
    }
}
