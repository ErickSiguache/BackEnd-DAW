using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string Clie_Nombre { get; set; }
        public string Clie_Apellido { get; set; }
        public string Telefono { get; set; }
        public string Clie_DUI { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
