using GourmetGo.Domain.DTOs.productos;
using GourmetGo.Domain.DTOs.ventas;
using GourmetGo.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmetGo.Application.Interfaces
{
    public interface IVentasServices
    {
        // Obtener todas las ventas
        Task<IEnumerable<ventas>> GetAllVentasAsync();

        // Obtener una venta por su ID
        Task<ventas> GetVentaByIdAsync(int id);

        // Crear una nueva venta a partir de un DTO
        Task<int> CreateVentaAsync(VentaCreateDto dto);

        // Actualizar una venta por su ID, usando un DTO
        Task<bool> UpdateVentaAsync(int id, VentaUpdateDto dto);

        // Eliminar una venta por su ID
        Task<bool> DeleteVentaAsync(int id);

        // Abrir una nueva mesa y asociar una venta pendiente
        Task<int> AbrirMesaAsync(int idMesa);

        // Agregar un producto a los detalles de una venta existente
        Task AgregarProductoADetalleAsync(AgregarProductoDto dto);

        Task CerrarVentaAsync(int idVenta);
    }
}

