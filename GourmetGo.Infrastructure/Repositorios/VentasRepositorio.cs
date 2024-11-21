using GourmetGo.Domain.DTOs.productos;
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
        
               private readonly IProductosRepository _productosRepository;
    
               private readonly IDetalleVentaRepository _detalleVentaRepository;


        public VentaRepository(AppDbContext context,IMesaRepository repository,IProductosRepository productosRepository,IDetalleVentaRepository detalleVentaRepository)
            {
                _context = context;
                mesaRepository = repository;
            _productosRepository = productosRepository;
            _detalleVentaRepository = detalleVentaRepository;
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
            if (dto.Total < 0)
            {
                throw new ArgumentException("El total no puede ser negativo.");
            }

            // Validar estado permitido
            var estadosPermitidos = new[] { "pendiente", "pagada" };
            if (!estadosPermitidos.Contains(dto.Estado.ToLower()))
            {
                throw new ArgumentException("Estado inválido.");
            }

            // Validar transición de estado
            var venta = await GetVentaByIdAsync(id);
            if (venta == null)
            {
                throw new InvalidOperationException("La venta no existe.");
            }

            if (venta.estado == "pagada" && dto.Estado.ToLower() == "pendiente")
            {
                throw new InvalidOperationException("No se puede cambiar una venta pagada a pendiente.");
            }

              await _context.Ventas.FindAsync(id);
          
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

            public async Task ActualizarVentaAsync(AgregarProductoDto dto)
            {
            // Validar cantidad positiva
            if (dto.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0.");
            }

            // Validar venta pendiente
            var venta = await GetVentaPendientePorMesaAsync(dto.id_mesa);
            if (venta == null)
            {
                throw new InvalidOperationException("La venta no existe o no está pendiente.");
            }

            // Validar producto y stock
            var producto = await _productosRepository.GetByIdAsync(dto.IdProducto);
            if (producto == null || producto.stock < dto.Cantidad)
            {
                throw new InvalidOperationException("El producto no existe o no tiene suficiente stock.");
            }

            // Calcular subtotal de manera explícita
            var subtotal = (decimal)dto.Cantidad * (decimal)producto.precio;

            // Agregar detalle de venta
            var detalle = new detalles_venta
            {
                id_venta = dto.IdVenta,
                id_producto = dto.IdProducto,
                cantidad = dto.Cantidad,
                precio_unitario = (decimal)producto.precio,
                subtotal = subtotal
            };

            await _detalleVentaRepository.AgregarDetalleAsync(detalle);

            // Actualizar stock del producto
            producto.stock -= dto.Cantidad;
            await _productosRepository.UpdateAsync(producto);

            // Actualizar total de la venta
            venta.total += subtotal;
            await Actualizar(venta);
        }

            public async Task CerrarVentaAsync(int idVenta)
        {
            // Obtener la venta
            var venta = await GetVentaByIdAsync(idVenta);
            if (venta == null)
            {
                throw new InvalidOperationException("La venta especificada no existe.");
            }

            // Verificar si ya está cerrada
            if (venta.estado == "pagada")
            {
                throw new InvalidOperationException("La venta ya está cerrada.");
            }

            // Actualizar el estado de la venta a "pagada"
            venta.estado = "pagada";
            await Actualizar(venta);


            // Cambiar el estado de la mesa a disponible (libre)
            await mesaRepository.CambiarEstadoMesaAsync(venta.id_mesa, false);
        }

            private async Task Actualizar(ventas ventas)
        {
            _context.Ventas.Update(ventas);
            await _context.SaveChangesAsync();

        }

            public async Task<int> AbrirMesaAsync(int idMesa)
        {
            // Verificar si ya existe una venta pendiente para la mesa
            var venta = await GetVentaPendientePorMesaAsync(idMesa);
            if (venta != null)
            {
                throw new InvalidOperationException("La mesa está abierta. No se puede abrir otra venta, ya hay una venta abierta para mesa con el id : " + venta.id_mesa + " esta asociada con el id de venta  : " + venta.id_venta); // Si existe, retorna la venta pendiente
            }

            // Verificar si la mesa está ocupada
            var mesaOcupada = await mesaRepository.EsMesaOcupadaAsync(idMesa); // Implementar este método
            if (mesaOcupada)
            {
                throw new InvalidOperationException("La mesa está ocupada. No se puede abrir otra venta.");
            }

            // Crear una nueva venta pendiente
            var nuevaVenta = new ventas
            {
                id_usuario = 1,
                id_mesa = idMesa,
                id_metodo = 1,
                estado = "pendiente",
                fecha = DateTime.UtcNow,
                total = 0
            };


            return await CreateVentaAsync(nuevaVenta);
        }


    }

  }

