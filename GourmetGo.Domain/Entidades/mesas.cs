using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Entidades
{
    public class mesas
    {
        [Key]
        public int id_mesa { get; set; }
        public int numero_mesa { get; set; }
        public int capacidad { get; set; }
        public bool estado { get; set; }
        public string descripcion {get; set;}

    }
}
