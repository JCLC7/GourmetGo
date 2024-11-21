using GourmetGo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Interfaces
{
    public interface IMetodosPagoRepository
    {
      
            Task<IEnumerable<metodos_pago>> GetAllAsync();
            Task<metodos_pago> GetByIdAsync(int id);
            Task<int> CreateAsync(metodos_pago metodoPago);
            Task<bool> UpdateAsync(int id, metodos_pago metodoPago);
            Task<bool> DeleteAsync(int id);
        

    }
}
