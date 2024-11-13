using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.Entidades
{
    public class Producto
    {
        [Key]
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }
        public int stock { get;set; }
        public int id_categoria { get; set; }
        public DateTime fecha_registro { get; set; }
        public string? imagen_url { get; set; }

    }
}
