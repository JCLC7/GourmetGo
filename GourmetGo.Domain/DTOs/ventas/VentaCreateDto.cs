using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.DTOs.ventas
{
    public class VentaCreateDto
    {
        public int IdUsuario { get; set; }
        public int IdMesa { get; set; }
        public int IdMetodo { get; set; }
        public decimal Total { get; set; }
    }

}
