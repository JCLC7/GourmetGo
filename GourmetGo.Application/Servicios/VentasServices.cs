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



        public async Task<bool> UpdateVentaAsync(int id, VentaUpdateDto dto)  => await _ventaRepository.UpdateVentaAsync(id, dto);
      



        public async Task<bool> DeleteVentaAsync(int id) =>  await _ventaRepository.DeleteVentaAsync(id);


        public async Task<int> AbrirMesaAsync(int idMesa) => await _ventaRepository.AbrirMesaAsync(idMesa);
      


        public async Task AgregarProductoADetalleAsync(AgregarProductoDto dto) => await _ventaRepository.ActualizarVentaAsync(dto);
      

        public async Task CerrarVentaAsync(int idVenta) => await _ventaRepository.CerrarVentaAsync(idVenta);


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
