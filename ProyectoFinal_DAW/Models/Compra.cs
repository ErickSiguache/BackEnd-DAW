using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Compra
    {
        [Key]
        public int Id_Compra { get; set; }
        public int Id_Proveedor { get; set; }
        public int Id_TipoCompra { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal TotalCompra { get; set; }
        public char Comp_Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<DetalleCompra> detalleCompras { get; set; }
    }
}
