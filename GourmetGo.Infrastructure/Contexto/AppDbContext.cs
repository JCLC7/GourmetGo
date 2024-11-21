using GourmetGo.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Infrastructure.Contexto
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<usuarios> Usuarios { get; set; }
        public DbSet<mesas> Mesas { get; set; }
        public DbSet<ventas> Ventas { get; set; }
        public DbSet<detalles_venta> detalles_venta{ get; set; }
        public DbSet<metodos_pago> metodos_pago { get; set; }

        public DbSet<categorias> categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración adicional si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}
