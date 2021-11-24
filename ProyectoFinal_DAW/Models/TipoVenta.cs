using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class TipoVenta
    {
        [Key]
        public int Id_TipoVenta { get; set; }
        public string TipV_Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
