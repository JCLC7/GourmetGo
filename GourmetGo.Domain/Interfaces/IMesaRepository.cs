using GourmetGo.Domain.DTOs.mesas;
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

        // Agregar una nueva mesa
        Task<mesas> AddMesasAsync(mesas mesas);


        // Obtener todas las mesas
        Task<IEnumerable<mesas>> GetAllAsync();


        // Obtener una mesa por su ID
        Task<mesas> ObtenerMesaPorIdAsync(int idMesa);


        // Cambiar el estado de una mesa
        Task CambiarEstadoMesaAsync(int idMesa, bool nuevoEstado);
     

        // Verificar si una mesa está ocupada
        Task<bool> EsMesaOcupadaAsync(int idMesa);
       

        // Método privado para validar la existencia de una mesa
        Task<mesas> VerificarMesaExiste(int idMesa);
        
         

    }
}
