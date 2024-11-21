using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Entidades
{
    public class metodos_pago
    {
        [Key]
        public int id_metodo { get; set; }
        [Required]
        [MaxLength(50)]
        public string metodo { get; set; }
    }
}
