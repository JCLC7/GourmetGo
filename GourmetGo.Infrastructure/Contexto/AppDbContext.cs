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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración adicional si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}
