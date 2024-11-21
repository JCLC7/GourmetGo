using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GourmetGo.Domain.DTOs.metodospago
{


    public class MetodoPagoDto
    {
        [Required]
        [MaxLength(50)]
        public string Metodo { get; set; }
    }

}
