using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class DetalleCompra
    {
        [Key]
        public int Id_DetalleCompra { get; set; }
        public int Id_Compra { get; set; }
        public int Id_LoteProducto { get; set; }
        public int Comp_Cantidad { get; set; }
        public decimal Comp_Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
