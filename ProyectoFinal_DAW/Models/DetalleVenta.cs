using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class DetalleVenta
    {
        [Key]
        public int Id_DetalleVenta { get; set; }
        public int Id_Venta { get; set; }
        public int Id_LoteProducto { get; set; }
        public int Vent_Cantidad { get; set; }
        public decimal Vent_Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
