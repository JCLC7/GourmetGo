using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs.ventas;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Servicios
{
    public class DetalleVentasSercives : IDetalleVentaService
    {
        private readonly IDetalleVentaRepository _repository;

        public DetalleVentasSercives(IDetalleVentaRepository repository)
        {
            _repository = repository;
        }

        public Task<List<DetalleVentaDto>> ObtenerDetallesPorVentaAsync(int idVenta)
        {
            throw new NotImplementedException();
        }

        async Task<object> IDetalleVentaService.GetDetallesConTotalByVentaIdAsync(int idVenta)
        {
            return await _repository.GetDetallesConTotalByVentaIdAsync(idVenta);
        }

      

       
    }
}
