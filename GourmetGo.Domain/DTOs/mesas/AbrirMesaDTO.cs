using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.DTOs.mesas
{
    public class AbrirMesaDto
    {
        public int IdMesa { get; set; } // ID de la mesa
        public int IdUsuario { get; set; } // ID del usuario que abre la mesa
    }
}
