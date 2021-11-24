using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class TipoCompra
    {
        [Key]
        public int Id_TipoCompra { get; set; }
        public string TipC_Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
