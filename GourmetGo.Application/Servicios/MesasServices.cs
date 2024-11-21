using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.DTOs.mesas;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        // Agregar una nueva mesa
        public async Task<mesas> AddMesaAsync(MesaDTO mesaDto)
        {
            // Validación de los datos del DTO
            if (mesaDto == null)
            {
                throw new ArgumentNullException(nameof(mesaDto), "El DTO de la mesa no puede ser nulo.");
            }

            if (mesaDto.NumeroMesa <= 0)
            {
                throw new ArgumentException("El número de la mesa debe ser mayor a 0.");
            }

            // Crear la entidad `mesas` a partir del DTO
            var mesa = new mesas
            {
                numero_mesa = mesaDto.NumeroMesa,
                capacidad = mesaDto.capacidad,
                estado = mesaDto.estado,
                descripcion = mesaDto.descripcion
            };

            return await _repository.AddMesasAsync(mesa);
        }

        // Obtener todas las mesas
        public async Task<IEnumerable<mesas>> GetAllMesasAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Actualizar el estado de una mesa
        public async Task UpdateMesaEstadoAsync(int idMesa, MesaDTO mesaDto)
        {
            // Validación de los datos
            if (mesaDto == null)
            {
                throw new ArgumentNullException(nameof(mesaDto), "El DTO de la mesa no puede ser nulo.");
            }

            if (idMesa <= 0)
            {
                throw new ArgumentException("El ID de la mesa debe ser mayor a 0.");
            }

            await _repository.CambiarEstadoMesaAsync(idMesa, mesaDto.estado);
        }
    }
}

