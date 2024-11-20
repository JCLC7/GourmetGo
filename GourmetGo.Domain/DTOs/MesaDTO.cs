using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.DTOs
{
    public class MesaDTO
    {
        public int NumeroMesa {  get; set; }
        public int capacidad { get; set; }
        public bool estado { get; set; }
        public string descripcion { get; set; }
    }
}
