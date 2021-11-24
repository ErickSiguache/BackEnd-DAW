using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class LoteProducto
    {
        [Key]
        public int Id_LoteProducto { get; set; }
        public int Id_Producto { get; set; }
        public DateTime FechaCaducacion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public int StockComprado { get; set; }
        public int StockVendido { get; set; }
        public char Lot_Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
