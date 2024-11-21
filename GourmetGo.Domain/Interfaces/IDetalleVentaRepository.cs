using GourmetGo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Interfaces
{
    public interface IDetalleVentaRepository
    {
     Task<object> GetDetallesConTotalByVentaIdAsync(int idVenta);
      Task AgregarDetalleAsync(detalles_venta detalle);
     Task<List<detalles_venta>> ObtenerDetallesPorVentaAsync(int idVenta);
    }
}
