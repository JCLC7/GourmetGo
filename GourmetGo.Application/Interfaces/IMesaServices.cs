using GourmetGo.Domain.DTOs.mesas;
using GourmetGo.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmetGo.Application.Interfaces
{
    public interface IMesaServices
    {
        // Agregar una nueva mesa
        Task<mesas> AddMesaAsync(MesaDTO mesaDto);

        // Obtener todas las mesas
        Task<IEnumerable<mesas>> GetAllMesasAsync();

        // Actualizar el estado de una mesa
        Task UpdateMesaEstadoAsync(int idMesa, MesaDTO mesaDto);
    }
}

