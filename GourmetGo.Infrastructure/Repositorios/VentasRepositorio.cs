using GourmetGo.Domain.DTOs.ventas;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Infrastructure.Repositorios
{

    public class VentaRepository : IVentaRepository
    {
            private readonly AppDbContext  _context;
        private readonly IMesaRepository mesaRepository;

            public VentaRepository(AppDbContext context,IMesaRepository repository)
            {
                _context = context;
            mesaRepository = repository;
            }

            public async Task<IEnumerable<ventas>> GetAllVentasAsync()
            {
                return await _context.Ventas.ToListAsync();
            }

            public async Task<ventas> GetVentaByIdAsync(int id)
            {
                return await _context.Ventas.FindAsync(id);
            }

        public async Task<int> CreateVentaAsync(ventas venta)
        {
            var mesa = await _context.Mesas.FindAsync(venta.id_mesa);
            if (mesa == null)
            {
                throw new InvalidOperationException("La mesa especificada no existe.");
            }

            if (mesa.estado)
            {
                throw new InvalidOperationException("La mesa ya está ocupada. No se puede crear otra venta.");
            }

            _context.Ventas.Add(venta);
            await mesaRepository.CambiarEstadoMesaAsync(venta.id_mesa, true);
            await _context.SaveChangesAsync();

            return venta.id_venta;
        }


        public async Task<bool> UpdateVentaAsync(int id, VentaUpdateDto dto)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return false; // La venta no existe
            }

            // Validar que el nuevo estado sea válido
            var estadosValidos = new[] { "pendiente", "pagada" };
            if (!estadosValidos.Contains(dto.Estado))
            {
                throw new InvalidOperationException("El estado proporcionado no es válido.");
            }

            // Actualizar solo los campos permitidos
            venta.total = dto.Total;
            venta.estado = dto.Estado;
            venta.fecha = dto.Fecha;

            _context.Ventas.Update(venta);
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<bool> DeleteVentaAsync(int id)
        {
            var hasDetails = await _context.detalles_venta.AnyAsync(d => d.id_venta == id); // Cambiado a AnyAsync
            if (hasDetails)
            {
                throw new InvalidOperationException("No se puede eliminar una venta con detalles asociados.");
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return false;

            _context.Ventas.Remove(venta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ventas> GetVentaPendientePorMesaAsync(int idMesa)
        {
            var ventaPendiente = await _context.Ventas
                .FirstOrDefaultAsync(v => v.id_mesa == idMesa && v.estado == "pendiente");


            return ventaPendiente;
        }



        public async Task ActualizarVentaAsync(ventas venta)
        {
            _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();
        }

      
   
        

     

    }

  }

