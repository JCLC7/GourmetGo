using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs.productos;
using GourmetGo.Domain.DTOs.ventas;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Servicios
{
    public class VentaService : IVentasServices
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductosRepository _productosRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IDetalleVentaRepository _detalleVentaRepository;

        public VentaService(IVentaRepository ventaRepository, IProductosRepository productosRepository, IMesaRepository mesaRepository, IDetalleVentaRepository detalleVentaRepository)
        {
            _ventaRepository = ventaRepository;
            _productosRepository = productosRepository;
            _mesaRepository = mesaRepository;
            _detalleVentaRepository = detalleVentaRepository;
        }

        public async Task<IEnumerable<ventas>> GetAllVentasAsync() =>   await _ventaRepository.GetAllVentasAsync();
        

        public async Task<ventas> GetVentaByIdAsync(int id)  =>  await _ventaRepository.GetVentaByIdAsync(id);


        public async Task<int> CreateVentaAsync(VentaCreateDto dto)
        {
            // Crear una nueva instancia de ventas usando los datos del DTO
            var nuevaVenta = new ventas
            {
                id_usuario = dto.IdUsuario,
                id_mesa = dto.IdMesa,
                id_metodo = dto.IdMetodo,
                estado = "pendiente",
                fecha = DateTime.UtcNow,
                total = dto.Total
            };

            return await _ventaRepository.CreateVentaAsync(nuevaVenta);
        }



        public async Task<bool> UpdateVentaAsync(int id, VentaUpdateDto dto)
        {
            // Validar el total
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
            var venta = await _ventaRepository.GetVentaByIdAsync(id);
            if (venta == null)
            {
                throw new InvalidOperationException("La venta no existe.");
            }

            if (venta.estado == "pagada" && dto.Estado.ToLower() == "pendiente")
            {
                throw new InvalidOperationException("No se puede cambiar una venta pagada a pendiente.");
            }

            return await _ventaRepository.UpdateVentaAsync(id, dto);
        }



        public async Task<bool> DeleteVentaAsync(int id) =>  await _ventaRepository.DeleteVentaAsync(id);


        public async Task<int> AbrirMesaAsync(int idMesa)
        {
            // Verificar si ya existe una venta pendiente para la mesa
            var venta = await _ventaRepository.GetVentaPendientePorMesaAsync(idMesa);
            if (venta != null)
            {
                throw new InvalidOperationException("La mesa está abierta. No se puede abrir otra venta, ya hay una venta abierta para mesa con el id : "+venta.id_mesa+" esta asociada con el id de venta  : " + venta.id_venta); // Si existe, retorna la venta pendiente
            }

            // Verificar si la mesa está ocupada
            var mesaOcupada = await _mesaRepository.EsMesaOcupadaAsync(idMesa); // Implementar este método
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
          

            return await _ventaRepository.CreateVentaAsync(nuevaVenta);
        }


        public async Task AgregarProductoADetalleAsync(AgregarProductoDto dto)
        {
            // Validar cantidad positiva
            if (dto.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0.");
            }

            // Validar venta pendiente
            var venta = await _ventaRepository.GetVentaPendientePorMesaAsync(dto.id_mesa);
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
            await _ventaRepository.ActualizarVentaAsync(venta);
        }

        /*public async Task AgregarProductoADetalleAsync(AgregarProductoDto dto)
{
    using var transaction = await _context.Database.BeginTransactionAsync();

    try
    {
        // Resto de la lógica...
        await _detalleVentaRepository.AgregarDetalleAsync(detalle);
        producto.stock -= dto.Cantidad;
        await _productosRepository.UpdateAsync(producto);
        venta.total += subtotal;
        await _ventaRepository.ActualizarVentaAsync(venta);

        await transaction.CommitAsync();
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}*/


    }


}
