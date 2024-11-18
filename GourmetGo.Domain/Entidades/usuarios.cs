using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Entidades
{
    public class usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string  rol { get; set; }
        public DateTime fecha_registro { get; set; } = DateTime.Now;

    
    }
}
