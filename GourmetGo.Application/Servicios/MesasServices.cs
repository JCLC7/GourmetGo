using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Servicios
{
    public class MesasServices : IMesaServices
    {
        private readonly IMesaRepository _repository;

        public MesasServices(IMesaRepository mesaRepository)
        {
            _repository = mesaRepository;
        }
        public async Task<mesas> AddMesaAsync(MesaDTO mesaDto) {
            var mesa = new mesas
            {
                numero_mesa = mesaDto.NumeroMesa,
                capacidad = mesaDto.capacidad,
                estado = mesaDto.estado,
                descripcion = mesaDto.descripcion
            };
            return await _repository.addMesas(mesa);
        } 

        public Task<IEnumerable<mesas>> GetAllTables()=> _repository.GetAllAsync();
      

        public Task<bool> UpdateDisponibilidad(int id, MesaDTO mesaDto) => _repository.updateDispo(id, mesaDto);
        
    }
}
