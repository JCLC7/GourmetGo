using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Application.Servicios
{
    public class Productoservices: IProductosservices
    {
        private readonly IProductosRepository _repository;

        public Productoservices(IProductosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Producto> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(Producto producto) => await _repository.AddAsync(producto);
        public async Task UpdateAsync(Producto producto) => await _repository.UpdateAsync(producto);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

    }
}
