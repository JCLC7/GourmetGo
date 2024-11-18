using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GourmetGo.Domain.Entidades
{
    public class Producto
    {
        [Key]
        public int id_producto { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        public float precio { get; set; }
        public int stock { get;set; }
        [Required(ErrorMessage = "El id_categoria es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El id_categoria debe ser mayor que 0.")]
        public int id_categoria { get; set; }
        [Required(ErrorMessage = "la fecha es obligatoria")]
        public DateTime fecha_registro { get; set; }
     
        public string? imagen_url { get; set; }

    }
}
