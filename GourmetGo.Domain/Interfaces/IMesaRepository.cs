using GourmetGo.Domain.DTOs;
using GourmetGo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Interfaces
{
    public interface IMesaRepository
    {
        Task<mesas> addMesas(mesas mesas);
        Task<bool> updateDispo(int id, MesaDTO mesaDTO);
        Task<IEnumerable<mesas>> GetAllAsync();

    }
}
