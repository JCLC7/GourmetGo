using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Interfaces
{
    public interface IMesaServices
    {
        Task<IEnumerable<mesas>> GetAllTables();
        Task<bool> UpdateDisponibilidad(int id, MesaDTO mesaDto);
        Task<mesas> AddMesaAsync(MesaDTO mesaDto);

    }
}
