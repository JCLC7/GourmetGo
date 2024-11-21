using GourmetGo.Domain.DTOs.metodospago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<IEnumerable<MetodoPagoDto>> GetAllAsync();
        Task<MetodoPagoDto> GetByIdAsync(int id);
        Task<int> CreateAsync(MetodoPagoDto metodoPagoDto);
        Task<bool> UpdateAsync(int id, MetodoPagoDto metodoPagoDto);
        Task<bool> DeleteAsync(int id);
    }

}
