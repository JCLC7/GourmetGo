using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.DTOs.ventas
{
    public class VentaUpdateDto
    {
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
