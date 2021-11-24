using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Venta
    {
        [Key]
        public int Id_Venta { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_TipoVenta { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public char Vent_Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
