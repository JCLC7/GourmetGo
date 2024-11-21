using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Entidades
{
    public class ventas
    {
        [Key]
        public int id_venta { get; set; }
        [Required]
        public int id_mesa { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [Required]
        public int id_metodo { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal total { get; set; }

        [Required]
        [MaxLength(20)]
        public string estado { get; set; } // "pendiente", "pagada"

        [Required]
        public DateTime fecha { get; set; }
    }
}
