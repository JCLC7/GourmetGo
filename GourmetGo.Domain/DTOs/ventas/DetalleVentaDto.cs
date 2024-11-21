using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.DTOs.ventas
{
    public class DetalleVentaDto
    {
        public int IdDetalle { get; set; } // Identificador único del detalle de venta
        public int IdVenta { get; set; } // Identificador de la venta asociada
        public int IdProducto { get; set; } // Identificador del producto asociado
        public string NombreProducto { get; set; } // Nombre del producto (opcional para vistas)
        public int Cantidad { get; set; } // Cantidad de producto en el detalle
        public decimal PrecioUnitario { get; set; } // Precio por unidad del producto
        public decimal Subtotal { get; set; } // Total calculado como Cantidad * PrecioUnitario
    }
}
