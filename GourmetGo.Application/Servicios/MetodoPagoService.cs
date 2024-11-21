using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs.metodospago;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Servicios
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodosPagoRepository _repository;

        public MetodoPagoService(IMetodosPagoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MetodoPagoDto>> GetAllAsync()
        {
            var metodos = await _repository.GetAllAsync();
            return metodos.Select(m => new MetodoPagoDto { Metodo = m.metodo });
        }

        public async Task<MetodoPagoDto> GetByIdAsync(int id)
        {
            var metodo = await _repository.GetByIdAsync(id);
            if (metodo == null) return null;

            return new MetodoPagoDto { Metodo = metodo.metodo };
        }

        public async Task<int> CreateAsync(MetodoPagoDto metodoPagoDto)
        {
            var metodoPago = new metodos_pago { metodo = metodoPagoDto.Metodo };
            return await _repository.CreateAsync(metodoPago);
        }

        public async Task<bool> UpdateAsync(int id, MetodoPagoDto metodoPagoDto)
        {
            var metodoPago = new metodos_pago { metodo = metodoPagoDto.Metodo };
            return await _repository.UpdateAsync(id, metodoPago);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}
