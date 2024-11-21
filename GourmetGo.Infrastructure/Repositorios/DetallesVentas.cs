using GourmetGo.Application.Interfaces;
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
    public class DetallesVentas: IDetalleVentaRepository
    {
        private readonly AppDbContext _context;
        public DetallesVentas(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetDetallesConTotalByVentaIdAsync(int idVenta)
        {
            var detalles = await _context.detalles_venta
                .Where(d => d.id_venta == idVenta)
                .Include(d => d.Producto) // Cargar información del producto relacionado
                .Select(d => new
                {
                    d.Producto.nombre, // Nombre del producto
                    d.cantidad,
                    d.precio_unitario,
                    d.subtotal
                })
                .ToListAsync();

            var total = detalles.Sum(d => d.subtotal); // Calcular el total general

            return new
            {
                Detalles = detalles,
                Total = total
            };
        }

        public async Task AgregarDetalleAsync(detalles_venta detalle)
        {
            _context.detalles_venta.Add(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task<List<detalles_venta>> ObtenerDetallesPorVentaAsync(int idVenta)
        {
            return await _context.detalles_venta
                .Where(d => d.id_venta == idVenta)
                .ToListAsync();
        }
    }

}
