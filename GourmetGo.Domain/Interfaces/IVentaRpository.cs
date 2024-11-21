using GourmetGo.Domain.DTOs.productos;
using GourmetGo.Domain.DTOs.ventas;
using GourmetGo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Interfaces
{
    public interface IVentaRepository
    {

        Task<IEnumerable<ventas>> GetAllVentasAsync();
        Task<ventas> GetVentaByIdAsync(int id);
        Task<int> CreateVentaAsync(ventas venta);
        Task<bool> UpdateVentaAsync(int id, VentaUpdateDto dto);
        Task<bool> DeleteVentaAsync(int id);
        Task<ventas> GetVentaPendientePorMesaAsync(int idMesa);
        //TRATAR DE CORREGIR ESTA TASK ACTUALZIARVENTAS ASYNC
        Task ActualizarVentaAsync(AgregarProductoDto dto);
        Task CerrarVentaAsync(int idVenta);
        Task<int> AbrirMesaAsync(int idMesa);

    }

    
}
