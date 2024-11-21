using GourmetGo.Domain.DTOs.ventas;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Interfaces
{
    public interface IDetalleVentaService
    {
         Task<object> GetDetallesConTotalByVentaIdAsync(int idVenta);
        Task<List<DetalleVentaDto>> ObtenerDetallesPorVentaAsync(int idVenta);


    }
}
